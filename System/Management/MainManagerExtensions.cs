using System.Linq;
using Metozis.Scripting.MScript;
using Metozis.System.Extensions;
using Metozis.System.Extensions.Copy;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Generators.Meta;
using Metozis.System.Generators.StellarGeneration;
using Metozis.System.Meta.Templates;
using Metozis.System.Meta;
using Metozis.System.Meta.Movement;
using Metozis.System.Meta.Templates.VFX;
using Metozis.System.Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Management
{
    public partial class MainManager
    {
        [BoxGroup("Debug")] 
        public GalaxyGenerationOptions testGalaxy;
        [BoxGroup("Debug")]
        public OrbitalEllipseSettings testArguments;
        [BoxGroup("Debug")]
        public StellarSystemGenerationOptions testSystem;

        [BoxGroup("Debug")] 
        [CopyDestination("Generation/Planet/Template")]
        public PlanetTemplate testPlanet;
        [BoxGroup("Debug")] 
        public AsteroidBeltTemplate testAsteroids;
        [BoxGroup("Debug")] 
        public SystemMetaGenerationSettings testSystemMetaGeneration;

        partial void Debug()
        {
            Galaxies.Add(ManagersRoot.Get<SpawnManager>().GenerateGalaxy(new Vector3(10, 10, 10), testGalaxy));
            var settings = new ShapeMovementController();
            settings.Center.Value = transform;
            settings.ShapeType = new SerializableSystemType(typeof(OrbitalEllipse));
            settings.Arguments = testArguments;
            
            var planetOptions = new PlanetGenerationOptions
            {
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
                Children =
                {
                    new PlanetGenerationOptions
                    {
                        Name = "Some planet Moon",
                        Guid = "",
                        Radius = 0.4f,
                        EnableTemplate = true,
                        MovementSettings = new ShapeMovementController
                        {
                            ShapeType = new SerializableSystemType(typeof(OrbitalEllipse)),
                            Arguments = new OrbitalEllipseSettings
                            {
                                AcendingNodeLongitude = 1,
                                AxisTransform = Vector3.one,
                                Eccentricity = 0,
                                Inclination = 0.35f,
                                EvaluationSpeed = 0.00005f,
                                MeanLongitude = 1,
                                PeriapsisArgument = 0.5f,
                                Segments = 80,
                                SemiMajorAxis = 0.4f
                            }
                        },
                        PathToPrefab = "Planet",
                        PhysicsSettings = new PhysicsSettings
                        {
                            Mass = 10,
                            Temperature = 100
                        },
                        Template = new PlanetTemplate
                        {
                            TemplateName = "Terran",
                            BaseHabitability = 60,
                            MinimumSize = new Vector3(0.7f, 0.7f, 0.7f)
                        },
                        Children =
                        {
                            new PlanetGenerationOptions
                            {
                                Name = "Some planet Moon Moon",
                                Guid = "",
                                Radius = 0.2f,
                                EnableTemplate = true,
                                MovementSettings = new ShapeMovementController
                                {
                                    ShapeType = new SerializableSystemType(typeof(OrbitalEllipse)),
                                    Arguments = new OrbitalEllipseSettings
                                    {
                                        AcendingNodeLongitude = 1,
                                        AxisTransform = Vector3.one,
                                        Eccentricity = 0.1f,
                                        Inclination = -0.35f,
                                        EvaluationSpeed = 0.0001f,
                                        MeanLongitude = 1,
                                        PeriapsisArgument = 0.2f,
                                        Segments = 80,
                                        SemiMajorAxis = 0.2f
                                    }
                                },
                                PathToPrefab = "Planet",
                                PhysicsSettings = new PhysicsSettings
                                {
                                    Mass = 10,
                                    Temperature = 100
                                },
                                Template = new PlanetTemplate
                                {
                                    TemplateName = "Terran",
                                    BaseHabitability = 60,
                                    MinimumSize = new Vector3(0.7f, 0.7f, 0.7f)
                                },
                            }
                        }
                    }
                }
            };
            
            var planetOptions2 = new PlanetGenerationOptions
            {
                Name = "Some planet 2",
                Guid = "",
                Radius = 0.25f,
                EnableTemplate = true,
                MovementSettings = new ShapeMovementController
                {
                    ShapeType = new SerializableSystemType(typeof(OrbitalEllipse)),
                    Arguments = new OrbitalEllipseSettings
                    {
                        AcendingNodeLongitude = 0.4f,
                        AxisTransform = Vector3.one,
                        Eccentricity = 0.1f,
                        Inclination = 0.3f,
                        EvaluationSpeed = 0.0005f,
                        MeanLongitude = 1,
                        PeriapsisArgument = 0.5f,
                        Segments = 80,
                        SemiMajorAxis = 3f
                    }
                },
                PhysicsSettings = new PhysicsSettings
                {
                    Mass = 100,
                    Temperature = 200
                },
                PathToPrefab = "Planet",
            };

            var starOptions = new StarGenerationOptions
            {
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
            };
            
            var starOptions2 = new StarGenerationOptions
            {
                Name = "Some Star 2",
                Guid = "",
                PhysicsSettings = new PhysicsSettings
                {
                    Mass = 200,
                    Temperature = 12000
                },
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
                PathToPrefab = "Star",
                Radius = 0.5f
            };
            
            var starOptions3 = new StarGenerationOptions
            {
                Name = "Some Star 3",
                Guid = "",
                PhysicsSettings = new PhysicsSettings
                {
                    Mass = 200,
                    Temperature = 12000
                },
                IgnoreRelativeGravity = true,
                MovementSettings = new ShapeMovementController
                {
                    ShapeType = new SerializableSystemType(typeof(OrbitalEllipse)),
                    Arguments = new OrbitalEllipseSettings
                    {
                        AcendingNodeLongitude = 0.4f,
                        AxisTransform = 5f.UniformVector(),
                        Eccentricity = 0,
                        Inclination = 0,
                        EvaluationSpeed = 0.00005f,
                        MeanLongitude = 1,
                        PeriapsisArgument = 0.5f,
                        Segments = 80,
                        SemiMajorAxis = 1
                    }
                },
                PathToPrefab = "Star",
                Radius = 0.2f
            };

            var systemOptions = new StellarSystemGenerationOptions
            {
                Name = "Test System",
                AdditionalDistanceFromRoot = 5,
                Guid = "",
                InitialSemiMajorAxis = 3,
                Members =
                {
                    planetOptions,
                    planetOptions2
                },
                OrbitSpace = 2,
                RootRelativeGravitation = true,
                RootSystem =
                {
                    starOptions,
                    starOptions2,
                    starOptions3
                }
            };

            //var json = Serializer.SerializeObject(systemOptions);
            //OutputUtils.WriteFile(Application.dataPath + "/Tests/Generation/test.json", json);

            //var mscriptReader = new MScriptReader();
            //mscriptReader.Read(Application.dataPath + "/Metozis/Scripting/MScript/Examples/example2.mscript");
            var generator = new SystemMetaGenerator();
            var system = ManagersRoot.Get<SpawnManager>().GenerateSystem(new Vector3(5,5, 5), generator.GenerateMeta(testSystemMetaGeneration) as StellarSystemGenerationOptions);
        }
    }
}