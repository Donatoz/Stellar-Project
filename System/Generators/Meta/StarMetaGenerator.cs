using Metozis.System.Generators.EntityGeneration;
using NotImplementedException = System.NotImplementedException;

namespace Metozis.System.Generators.Meta
{
    public sealed class StarMetaGenerator : IMetaGenerator
    {
        /*
            Name = "Some Star",
            Guid = "",
            PhysicsSettings = new PhysicsSettings
            {
                Mass = 500,
                Temperature = 23000
            },
            PathToPrefab = "Star",
            MovementSettings = new ShapeMovementController
            {
                ShapeType = new SerializableSystemType(typeof(OrbitalEllipse)),
                Arguments = new OrbitalEllipseSettings
                {
                    AcendingNodeLongitude = 0.4f,
                    AxisTransform = Vector3.one,
                    Eccentricity = 0,
                    Inclination = 0,
                    EvaluationSpeed = 0.005f,
                    MeanLongitude = 1,
                    PeriapsisArgument = 0.5f,
                    Segments = 80,
                    SemiMajorAxis = 3f
                }
            },
            Radius = 0.7f,
            AdditionalEffects =
            {
                testAsteroids
            }
         */
        
        public GenerationOptions GenerateMeta(MetaGenerationSettings args)
        {
            var starSettings = (StarMetaGenerationSettings) args;
            var result = (StarGenerationOptions) MetaSubGenerators.StellarBodySubGenerator(new StarGenerationOptions(), starSettings);

            result.PathToPrefab = "Star";
            
            return result;
        }
    }
}