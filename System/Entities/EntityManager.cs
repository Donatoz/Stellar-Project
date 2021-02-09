using System;
using System.Collections.Generic;

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
    }
}