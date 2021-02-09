using System;
using System.Collections.Generic;

namespace Metozis.System.Registry
{
    public sealed class RuleAggregator
    {
        public string StopSymbol;
        public Func<string, string> SubAggregation;
        
        private List<string> cache = new List<string>();
        
        public IReadOnlyList<string> Aggregate(string[] lines, int startIndex)
        {
            cache.Clear();
            for (var i = startIndex; i < lines.Length; i++)
            {
                var line = SubAggregation != null ? SubAggregation(lines[i]) : lines[i];
                if (line == StopSymbol) break;
                cache.Add(line);
            }

            return cache;
        }
    }
}