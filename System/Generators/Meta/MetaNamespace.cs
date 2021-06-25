using System.Collections.Generic;
using System.Linq;
using Metozis.System.Extensions;
using Sirenix.Utilities;

namespace Metozis.System.Generators.Meta
{
    public class MetaNamespace
    {
        private readonly Dictionary<string, bool> names;
        
        public MetaNamespace(params string[] names)
        {
            this.names = new Dictionary<string, bool>();
            foreach (var name in names)
            {
                this.names[name] = false;
            }
        }

        public string PickRandom(bool silent, bool unique)
        {
            var name = unique ? names.Keys.Where(k => names[k] == false).PickRandom() : names.Keys.PickRandom();
            if (!silent)
            {
                names[name] = true;
            }

            return name;
        }

        public void Reset()
        {
            names.Keys.ForEach(k => names[k] = false);
        }
    }
}