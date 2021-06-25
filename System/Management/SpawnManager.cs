using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using Metozis.System.Generators;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Generators.Preprocessors;
using Metozis.System.Generators.StellarGeneration;
using Metozis.System.Meta.Templates;
using Metozis.System.Stellar;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Metozis.System.Management
{
    public sealed class SpawnManager : SerializedMonoBehaviour
    {
        private readonly Dictionary<Type, EntityGenerator> generators = new Dictionary<Type, EntityGenerator>
        {
            {
                typeof(Star),
                new StarGenerator()
            },

            {
                typeof(Galaxy),
                new GalaxyGenerator()
            },
            
            {
                typeof(Planet),
                new PlanetGenerator()
            },

            {
                typeof(StellarSystem),
                new StellarSystemGenerator()
            }
        };

        private readonly Dictionary<Type, IGeneratorPostprocessor> postprocessors =
            new Dictionary<Type, IGeneratorPostprocessor>
            {
                {
                    typeof(Planet),
                    new StellarObjectPostprocessor()
                },
                
                {
                    typeof(Star),
                    new StellarObjectPostprocessor()
                }
            };
        
        private void Awake()
        {
            generators.ForEach(p => p.Value.Initialize());
        }

        public T Generate<T>(GenerationOptions options) where T : Entity
        {
            var postprocessor = postprocessors.ContainsKey(typeof(T)) ? postprocessors[typeof(T)] : null;
            return (T)generators[typeof(T)].Generate(options, postprocessor);
        }

        public Entity Generate(Type type, GenerationOptions options)
        {
            var postprocessor = postprocessors.ContainsKey(type) ? postprocessors[type] : null;
            return generators[type].Generate(options, postprocessor);
        }

        public GameObject InstantiateObject(GameObject obj, Vector3 pos, Quaternion rot)
        {
            return Instantiate(obj, pos, rot);
        }

        public T LoadResource<T>(string path) where T : Object
        {
            return (T)Resources.Load(path);
        }

        public Star GenerateStar(Vector3 position, StarGenerationOptions options)
        {
            var newStar = Generate<Star>(options);
            newStar.transform.position = position;
            return newStar;
        }

        public Galaxy GenerateGalaxy(Vector3 position, GalaxyGenerationOptions options)
        {
            var galaxy = Generate<Galaxy>(options);
            galaxy.transform.position = position;
            return galaxy;
        }

        public StellarSystem GenerateSystem(Vector3 position, StellarSystemGenerationOptions options)
        {
            var system = Generate<StellarSystem>(options);
            system.transform.position = position;
            return system;
        }

        public Planet GeneratePlanet(Vector3 pos, PlanetGenerationOptions options)
        {
            var planet = Generate<Planet>(options);
            planet.transform.position = pos;
            return planet;
        }

        public void RegisterGenerator<T>(Type entityType) where T : EntityGenerator, new()
        {
            generators[entityType] = new T();
        }
    }
}