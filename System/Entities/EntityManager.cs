using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Metozis.System.Entities
{
    public static class EntityManager
    {
        private static readonly Dictionary<string, Entity> entities = new Dictionary<string, Entity>();

        public static string RegisterEntity(Entity e)
        {
            var guid = Guid.NewGuid().ToString();
            entities[guid] = e;
            return guid;
        }
        
        [CanBeNull]
        public static Entity GetById(string id)
        {
            return entities[id];
        }
    }
}