using System;
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
        
        private struct MovementConfigurator
        {
            public Func<float> SemiMajorAxis;
        }
        
        public override Entity Generate(GenerationOptions options, IGeneratorPostprocessor postprocessor = null)
        {
            if (!Validate<StellarSystemGenerationOptions>(options)) return null;
            var genOptions = options as StellarSystemGenerationOptions;
            
            // Stage 1 : Generate system root
            
            var rootTransform = new GameObject(options.Name).transform;
            var systemRoot = new GameObject("Root");
            systemRoot.transform.parent = rootTransform;
            var membersRoot = new GameObject("Members");
            membersRoot.transform.parent = rootTransform;
            var effectsRoot = new GameObject("Effects");
            effectsRoot.transform.parent = rootTransform;
            var system = ManagersRoot.Get<SpawnManager>()
                .InstantiateObject(template, Vector3.zero, Quaternion.identity).GetComponent<StellarSystem>();
            var (rootObjects, membersObjects) = GenerateObjects(genOptions, systemRoot.transform, membersRoot.transform);
            
            // Stage 2 : Generate root objects

            system.Root = rootObjects;
            
            // Stage 3 : Generate members

            system.Members = membersObjects;

            return system;
        }
        
        /// <summary>
        /// Generate 2 lists of system objects ( 1st list - root, 2nd list - members )
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private (List<StellarObject>, List<StellarObject>) GenerateObjects(
            StellarSystemGenerationOptions options,
            Transform systemRoot,
            Transform membersRoot)
        {
            var generatedRoot = new List<StellarObject>();
            for (var i = 0; i < options.RootSystem.Count; i++)
            {
                var created = GenerateReproducibleStellarObject(options.RootSystem[i], systemRoot);

                if (created.GetModule<MovementModule>() != null)
                {
                    var movementShape = created.GetModule<MovementModule>().Settings.Value.Arguments
                        .As<ShapeSettings>();
                    movementShape.EvaluationProgress = (i + 1) / (float) options.RootSystem.Count;
                    
                    ConfigureMovement(created);
                }

                generatedRoot.Add(created);
            }

            var generatedMembers = new List<StellarObject>();
            for (var i = 0; i < options.Members.Count; i++)
            {
                var created = GenerateReproducibleStellarObject(options.Members[i], membersRoot);
                generatedRoot.Add(created);
            }

            return (generatedRoot, generatedMembers);
        }
        
        /// <summary>
        /// Generate a stellar object (which can have movement module) with all it's children (recursively).
        /// </summary>
        /// <param name="options"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private StellarObject GenerateReproducibleStellarObject(StellarBodyGenerationOptions options, Transform parent)
        {
            var path = options.PathToPrefab;
            var objTemplate = ManagersRoot.Get<SpawnManager>().LoadResource<GameObject>($"Templates/{path}");
            if (objTemplate == null)
            {
                Debug.LogWarning($"Unable to load template in [{path}]");
                return null;
            }

            var repr = objTemplate.GetComponent<IReproducible>();

            if (repr == null)
            {
                Debug.LogWarning($"Generated entity [{path}] is not reproducible");
                return null;
            }
            var type = repr.GetGeneratorType();
            
            var obj = (StellarObject) ManagersRoot.Get<SpawnManager>().Generate(type, options);
            var chilrenRoot = new GameObject("Children");
            chilrenRoot.transform.parent = obj.transform;
            obj.transform.parent = parent;

            if (obj.Meta.MovementSettings != null)
            {
                obj.AddModule(SetUpMovement(obj, parent));
            }

            foreach (var child in options.Children)
            {
                GenerateReproducibleStellarObject(child, chilrenRoot.transform);
            }
            
            return obj;
        }
        
        private MovementModule SetUpMovement(StellarObject o, Transform center)
        {
            var movement = o.AddModule(new MovementModule(o)) as MovementModule;
            movement.Settings.Value = o.Meta.MovementSettings;
            movement.Settings.Value.SetCenter(center);
            return movement;
        }

        private void ConfigureMovement(StellarObject o)
        {
            var movementShape = o.GetModule<MovementModule>().Settings.Value.Arguments.As<OrbitalEllipseSettings>();
        }
        
        /*

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
                    movement.Settings.Value.ChangePathVisual(0, default);
                    currentMaxSize = Mathf.Max(actual[i].Radius.Value, currentMaxSize);
                }
                else
                {
                    var diff = ((ShapeSettings) actual[i].Meta.MovementSettings.Arguments).AxisTransform.x - rootSize;
                    rootSize += actual[i].Radius.Value + diff;
                }

                foreach (Transform effect in actual[i].transform.Find("WorldEffects"))
                {
                    effect.transform.parent = systemRoot.Find("Effects").transform;
                    effect.transform.localPosition = Vector3.zero;
                }

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
        */
    }
}