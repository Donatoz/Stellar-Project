using System;
using Metozis.System.Extensions;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Meta;
using Random = UnityEngine.Random;

namespace Metozis.System.Generators.Meta
{
    public sealed class PlanetMetaGenerator : IMetaGenerator
    {
        private float distanceFromRoot;
        
        /*
            Name = "Some planet",
            Guid = "",
            MovementSettings = settings,
            PhysicsSettings = new PhysicsSettings
            {
                Mass = 100,
                Temperature = 200
            },
            Radius = 0.7f,
            PathToPrefab = "Planet",
            EnableTemplate = true,
            Template = testPlanet,
         */

        public void SetUp(float distanceFromRoot)
        {
            this.distanceFromRoot = distanceFromRoot;
        }
        
        public GenerationOptions GenerateMeta(MetaGenerationSettings args)
        {
            var planetSettings = (PlanetMetaGenerationSettings) args;
            var root = (PlanetGenerationOptions)GenerateSimplePlanet(planetSettings);
            var childrenAmount = Random.Range(planetSettings.ChildAmountRange.x, planetSettings.ChildAmountRange.y);

            
            
            return root;
        }

        private GenerationOptions GenerateSimplePlanet(PlanetMetaGenerationSettings args)
        {
            var result = (PlanetGenerationOptions) MetaSubGenerators.StellarBodySubGenerator(new PlanetGenerationOptions(), args);

            //result.Name = args.Namespace.PickRandom(false, true);
            result.Template = args.Templates.PickRandom();
            result.EnableTemplate = true;
            result.PathToPrefab = "Planet";

            return result;
        }
    }
}