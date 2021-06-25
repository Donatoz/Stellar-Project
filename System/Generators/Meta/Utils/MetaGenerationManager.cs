using System.Collections.Generic;

namespace Metozis.System.Generators.Meta.Utils
{
    public static class MetaGenerationManager
    {
        public static readonly HashSet<string> ExcludedProperties = new HashSet<string>();

        public static bool IsExcluded(string label)
        {
            return ExcludedProperties.Contains(label);
        }
    }
}