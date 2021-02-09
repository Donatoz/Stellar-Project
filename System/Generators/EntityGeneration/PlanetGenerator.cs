using Metozis.System.Entities;
using Metozis.System.Generators.Preprocessors;
using Metozis.System.Management;
using Metozis.System.Meta.Templates;
using UnityEngine;

namespace Metozis.System.Generators.EntityGeneration
{
    public class PlanetGenerator : EntityGenerator
    {
        protected override string TemplateName => "Planet";
        public override Entity Generate(GenerationOptions options, IGeneratorPostprocessor postprocessor = null)
        {
            if (!Validate<PlanetGenerationOptions>(options)) return null;
            var genOptions = (PlanetGenerationOptions) options;

            var planet = ManagersRoot.Get<SpawnManager>().InstantiateObject(template,
                Vector3.zero, Quaternion.identity).GetComponent<Planet>();
            planet.Meta = genOptions;
            planet.Radius.Value = genOptions.Radius;
            planet.name = genOptions.Name;

            if (genOptions.EnableTemplate)
            {
                planet.ApplyTemplate(PlanetTemplate.GetRandomFromPreferences());
            }

            postprocessor?.Resolve(planet, options);
            return planet;
        }
    }
}