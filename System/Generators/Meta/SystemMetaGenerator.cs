
using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using Metozis.System.Extensions;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Generators.Meta.Utils;
using Metozis.System.Generators.StellarGeneration;
using Metozis.System.Meta;
using Metozis.System.Meta.Movement;
using Metozis.System.Physics.Movement;
using Metozis.System.Shapes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Metozis.System.Generators.Meta
{
    public sealed class SystemMetaGenerator : IMetaGenerator
    {
        private struct GenerationMetaData
        {
            public int GeneratedStars;
            public float RootSize;
            public StellarBodyGenerationOptions PreviousGeneratedStar;
        }
        
        private readonly Dictionary<Type, IMetaGenerator> subGenerators = new Dictionary<Type, IMetaGenerator>
        {
            {
                typeof(PlanetMetaGenerationSettings),
                new PlanetMetaGenerator()
            },
            {
                typeof(StarMetaGenerationSettings),
                new StarMetaGenerator()
            }
        };
        
        public GenerationOptions GenerateMeta(MetaGenerationSettings args)
        {
            MetaGenerationManager.ExcludedProperties.Clear();
            args.RegisterProperties();
            
            var systemSettings = (SystemMetaGenerationSettings) args;
            var result = new StellarSystemGenerationOptions();

            //result.Name = args.Namespace.PickRandom(false, true);
            result.Name = "Obj";
            
            // Initial SMA
            var pickedSMA = systemSettings.InitialSemiMajorAxisVariants.PickRandom().Value;
            result.InitialSemiMajorAxis = Random.Range(pickedSMA.x, pickedSMA.y);
            
            // Orbit space
            var pickedOrbitSpace = systemSettings.OrbitSpaceVariants.PickRandom().Value;
            result.OrbitSpace = Random.Range(pickedOrbitSpace.x, pickedOrbitSpace.y);
            
            // Root orbit space multiplier
            var rootPickedOrbitSpaceMultiplier = systemSettings.RootOrbitSpaceMultiplierVariants.PickRandom().Value;
            var rootOrbitSpaceMultiplier =
                Random.Range(rootPickedOrbitSpaceMultiplier.x, rootPickedOrbitSpaceMultiplier.y);

            var pickedRootEvalSpeed = systemSettings.RootEvaluationSpeedVariants.PickRandom().Value;

            result.AdditionalDistanceFromRoot = Random.Range(systemSettings.AdditionalDistanceFromRoot.x, systemSettings.AdditionalDistanceFromRoot.y);
            result.RootSystem = new List<StellarBodyGenerationOptions>();
            var meta = new GenerationMetaData
            {
                GeneratedStars = 0,
                RootSize = result.AdditionalDistanceFromRoot,
                PreviousGeneratedStar = null
            };
            var rootSize = Random.Range(systemSettings.RootSize.x, systemSettings.RootSize.y);
            
            for (int i = 0; i < rootSize; i++)
            {
                var movement = new ShapeMovementController
                {
                    ShapeType = new SerializableSystemType(typeof(OrbitalEllipse)),
                    Arguments = new OrbitalEllipseSettings
                    {
                        AcendingNodeLongitude = Random.Range(0f, 4f),
                        AxisTransform = Vector3.one,
                        Eccentricity = 0,
                        Inclination = 0,
                        EvaluationSpeed = Random.Range(pickedRootEvalSpeed.x, pickedRootEvalSpeed.y),
                        MeanLongitude = 1,
                        PeriapsisArgument = 0.5f,
                        Segments = 80,
                        SemiMajorAxis = (systemSettings.RootSemiMajorAxisOverDistance.Evaluate((float)i/(float)rootSize)) * ((meta.PreviousGeneratedStar?.Radius ?? 0) + i * rootOrbitSpaceMultiplier)
                    }
                };
                var variant = systemSettings.StarGenerationVariants.PickRandom();
                var generated =
                    subGenerators[typeof(StarMetaGenerationSettings)].GenerateMeta(variant) as StarGenerationOptions;
                if (((OrbitalEllipseSettings)movement.Arguments).SemiMajorAxis > 0)
                {
                    generated.MovementSettings = movement;
                }

                generated.Radius *= systemSettings.RootObjectRadiusOverDistance.Evaluate((float) i / (float) rootSize);
                result.RootSystem.Add(generated);
                result.Stars.Add(generated);

                meta.PreviousGeneratedStar = generated;
                meta.RootSize += i * rootOrbitSpaceMultiplier;
                meta.GeneratedStars++;
            }

            var membersAmount = Random.Range(systemSettings.MembersAmount.x, systemSettings.MembersAmount.y);

            for (int i = 0; i < membersAmount; i++)
            {
                var movement = new ShapeMovementController
                {
                    ShapeType = new SerializableSystemType(typeof(OrbitalEllipse)),
                    Arguments = new OrbitalEllipseSettings
                    {
                        AcendingNodeLongitude = Random.Range(0f, 4f),
                        AxisTransform = Vector3.one,
                        Eccentricity = 0,
                        Inclination = Random.Range(0f, 2f) * systemSettings.MembersInclinationOverDistance.Evaluate((float)i / (float)membersAmount),
                        EvaluationSpeed = Random.Range(pickedRootEvalSpeed.x, pickedRootEvalSpeed.y),
                        MeanLongitude = 1,
                        PeriapsisArgument = 0.5f,
                        Segments = 80,
                        SemiMajorAxis = result.OrbitSpace * systemSettings.OrbitSpaceMulOverDistance.Evaluate((float)i/(float)membersAmount) * (i + 1) + meta.RootSize
                    }
                };
                
                var variant = systemSettings.PlanetGenerationVariants.PickRandom();
                var generated = subGenerators[variant.GetType()].GenerateMeta(variant) as PlanetGenerationOptions;
                generated.MovementSettings = movement;
                result.Members.Add(generated);
                result.Planets.Add(generated);
            }

            return result;
        }
    }
}