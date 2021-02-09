using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using Metozis.System.Generators.StellarGeneration;
using QuikGraph;
using UnityEngine;

namespace Metozis.System.Stellar
{
    public class Galaxy : Entity, IReproducible
    {
        public GalaxyVisualModule Visual => GetModule<GalaxyVisualModule>();
        
        public UndirectedGraph<GalaxyMapObject, Edge<GalaxyMapObject>> Structure;
        private Dictionary<string, GalaxyMapObject> systems = new Dictionary<string, GalaxyMapObject>();
        
        public IReadOnlyDictionary<string, GalaxyMapObject> Systems => systems;

        private void Start()
        {
            foreach (var system in Structure.Vertices)
            {
                systems[system.Guid] = system;
            }
        }
        
        protected override void InitializeModules()
        {
            AddModule(new GalaxyVisualModule(this));
        }

        public Type GetGeneratorType()
        {
            return typeof(Galaxy);
        }
    }
}