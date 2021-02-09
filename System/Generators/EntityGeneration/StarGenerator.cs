using Metozis.System.Entities;
using Metozis.System.Generators.Preprocessors;
using Metozis.System.Management;
using UnityEngine;

namespace Metozis.System.Generators.EntityGeneration
{
    public class StarGenerator : EntityGenerator
    {
        protected override string TemplateName => "Star";

        public override Entity Generate(GenerationOptions options, IGeneratorPostprocessor postprocessor = null)
        {
            if (!Validate<StarGenerationOptions>(options)) return null;
            var genOptions = options as StarGenerationOptions;

            var newStar = ManagersRoot.Get<SpawnManager>()
                .InstantiateObject(template, Vector3.zero, Quaternion.identity).GetComponent<Star>();
            
            newStar.Name = options.Name;
            newStar.Physics.Temperature.Value = genOptions.PhysicsSettings.Temperature;
            newStar.Physics.Mass.Value = genOptions.PhysicsSettings.Mass;
            newStar.Radius.Value = genOptions.Radius;
            newStar.Meta = genOptions;
            
            postprocessor?.Resolve(newStar, options);
            return newStar;
        }
    }
}