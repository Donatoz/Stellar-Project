using System.Collections.Generic;
using System.Linq;
using Metozis.System.Entities;
using Metozis.System.Extensions;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Generators.Preprocessors;
using Metozis.System.Management;
using Metozis.System.Meta.Movement;
using Metozis.System.Physics;
using Metozis.System.Physics.Movement;
using Metozis.System.Shapes;
using Metozis.System.Stellar;
using Metozis.System.VFX;
using Shapes;
using Sirenix.Utilities;
using UnityEngine;

namespace Metozis.System.Generators.StellarGeneration
{
    public class StellarSystemGenerator : EntityGenerator
    {
        protected override string TemplateName => "System";
        public override Entity Generate(GenerationOptions options, IGeneratorPostprocessor postprocessor = null)
        {
            if (!Validate<StellarSystemGenerationOptions>(options)) return null;
            var genOptions = options as StellarSystemGenerationOptions;
            
            // Stage 1 : Generate system root
            
            var rootTransform = new GameObject(options.Name).transform;
            var effectsRoot = new GameObject("Effects");
            effectsRoot.transform.parent = rootTransform;
            var system = ManagersRoot.Get<SpawnManager>()
                .InstantiateObject(template, Vector3.zero, Quaternion.identity).GetComponent<StellarSystem>();
            var root = GenerateRoot(genOptions);
            system.Root = root;
            system.Meta = genOptions;

            SetUpRootGravity(root.ToArray(), rootTransform, genOptions, out var rootSize);
            
            // Stage 2 : Generate members

            var mainMembers = new List<StellarObject>();
            var membersRoot = new GameObject($"{options.Name} Members");
            membersRoot.transform.parent = rootTransform;

            foreach (var member in genOptions.Members)
            {
                GenerateMember(member, ref mainMembers, membersRoot.transform, genOptions);
            }

            var allMembers = new List<StellarObject>();

            foreach (var member in mainMembers)
            {
                SetUpCommonGravity(member, membersRoot.transform, genOptions, ref allMembers, rootSize);
            }
            
            var lightSource = new GameObject("System light");
            lightSource.transform.parent = rootTransform;
            
            
            SetUpLight(allMembers, lightSource);
            
            system.Members = allMembers;
            return system;
        }

        private List<StellarObject> GenerateRoot(StellarSystemGenerationOptions options)
        {
            var generatedObjects = new List<StellarObject>();
            for (var i = 0; i < options.RootSystem.Count; i++)
            {
                var created = GenerateReproducibleStellarObject(options.RootSystem[i] as StellarBodyGenerationOptions);
                generatedObjects.Add(created);
            }

            return generatedObjects;
        }

        private void GenerateMember(StellarBodyGenerationOptions options, 
            ref List<StellarObject> rootObjects, 
            Transform root,
            StellarSystemGenerationOptions sysOptions)
        {
            var generated = GenerateReproducibleStellarObject(options);
            Transform transform;
            var childTransform = (transform = root.transform).Find("Children");
            generated.transform.parent = childTransform == null ? transform : childTransform;
            if (sysOptions.Members.Contains(options))
            {
                rootObjects.Add(generated);
            }
            foreach (var child in options.Children)
            {
                GenerateMember(child, ref rootObjects, generated.transform, sysOptions);
            }
        }

        private StellarObject GenerateReproducibleStellarObject(StellarBodyGenerationOptions options)
        {
            var path = options.PathToPrefab;
            var objTemplate = ManagersRoot.Get<SpawnManager>().LoadResource<GameObject>($"Templates/{path}");
            if (objTemplate == null)
            {
                Debug.Log($"Unable to load template in [{path}]");
                return null;
            }

            var repr = objTemplate.GetComponent<IReproducible>();

            if (repr == null)
            {
                Debug.Log($"Generated entity [{path}] is not reproducible");
                return null;
            }
            
            var type = repr.GetGeneratorType();
            var obj = (StellarObject) ManagersRoot.Get<SpawnManager>().Generate(type, options);
            var chilrenRoot = new GameObject("Children");
            chilrenRoot.transform.parent = obj.transform;
            return obj;
        }

        private void SetUpRootGravity(StellarObject[] rootObjects, 
            Transform systemRoot, 
            StellarSystemGenerationOptions options,
            out float rootSize)
        {
            rootSize = options.InitialSemiMajorAxis;
            if (rootObjects.Length == 1)
            {
                rootSize += rootObjects[0].Radius.Value + options.AdditionalDistanceFromRoot;
                rootObjects[0].transform.parent = systemRoot;
                return;
            }
            
            var ignored = rootObjects.Where(o => o.Meta.IgnoreRelativeGravity).ToArray();
            var actual = rootObjects.Where(o => !o.Meta.IgnoreRelativeGravity).ToArray();
            var currentMaxSize = 0f;

            for (int i = options.RootRelativeGravitation ? 0 : 1; i < actual.Length; i++)
            {
                var movement = SetUpMovement(actual[i], systemRoot);
                movement.Settings.Value.ChangePathVisual(default, Temperature.GetTemperatureColor(actual[i].Physics.Temperature.Value).LerpToBlack(0.7f).LerpToWhite(0.1f).Alpha(0.4f));
                
                if (options.RootRelativeGravitation)
                {
                    var progress = (float)i / (float)actual.Length;
                    var settings = (ShapeSettings) movement.Settings.Value.Arguments;
                    settings.EvaluationProgress = progress;
                    settings.AdditionalVisualRadius = 0.1f * i;
                }
                else
                {
                    rootSize += actual[i].Radius.Value +
                                ((ShapeSettings) actual[i].Meta.MovementSettings.Arguments).AxisTransform.x;
                }

                foreach (Transform effect in actual[i].transform.Find("WorldEffects"))
                {
                    effect.transform.parent = systemRoot.Find("Effects").transform;
                    effect.transform.localPosition = Vector3.zero;
                }

                currentMaxSize = Mathf.Max(actual[i].Radius.Value, currentMaxSize);
                actual[i].transform.parent = systemRoot;
            }

            if (options.RootRelativeGravitation)
            {
                rootSize += currentMaxSize;
            }

            for (var i = 0; i < ignored.Length; i++)
            {
                var movement = SetUpMovement(ignored[i], systemRoot);
                movement.Settings.Value.ChangePathVisual(default, Temperature.GetTemperatureColor(ignored[i].Physics.Temperature.Value).LerpToBlack(0.7f).LerpToWhite(0.1f).Alpha(0.4f));

                var diff = ((ShapeSettings) ignored[i].Meta.MovementSettings.Arguments).AxisTransform.x - rootSize;
                rootSize += ignored[i].Radius.Value + diff;
                ignored[i].transform.parent = systemRoot;
            }
            
            rootSize += options.AdditionalDistanceFromRoot;
            
            var rootBorder = ShapeUtils.DrawEllipse(rootSize, rootSize, systemRoot.position);
            rootBorder.name = "Root border";
            rootBorder.transform.parent = systemRoot;
            rootBorder.Renderer.Width.Value = 0.1f;
            rootBorder.Renderer.Color.Value = ManagersRoot.Get<Preferences>().RootBorderColor;
        }

        private MovementModule SetUpMovement(StellarObject o, Transform center)
        {
            var movement = o.AddModule(new MovementModule(o)) as MovementModule;
            movement.Settings.Value = o.Meta.MovementSettings;
            movement.Settings.Value.SetCenter(center);
            return movement;
        }

        private void SetUpCommonGravity(StellarObject obj, 
            Transform currentRoot,
            StellarSystemGenerationOptions options,
            ref List<StellarObject> allMembers,
            float rootSize)
        {
            var semiMajorAxis = options.InitialSemiMajorAxis;
            allMembers.Add(obj);
            
            if (obj.Meta.MovementSettings != null)
            {
                var movement = SetUpMovement(obj, currentRoot);
                movement.Settings.Value.ChangePathVisual(obj.Radius.Value / 15, default);

                ((ShapeSettings) movement.Settings.Value.Arguments).AxisTransform =
                    rootSize.UniformVector() + semiMajorAxis.UniformVector();
            }
            
            foreach (Transform child in obj.transform.Find("Children"))
            {
                SetUpCommonGravity(child.GetComponent<StellarObject>(), obj.transform, options, ref allMembers, rootSize);
            }
        }

        private void SetUpLight(IEnumerable<StellarObject> members, GameObject light)
        {
            ShapesExtensions.ForEach(members.OfType<ILightReceiver>(), m => m.SetLightPoint(light));
        }
    }
}