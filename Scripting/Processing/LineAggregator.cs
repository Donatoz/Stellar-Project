using System;
using System.Collections.Generic;
using Metozis.Scripting.Injecting;

namespace Metozis.Scripting.Processing
{
    public class LineAggregator : IRuleInjective
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
        
        public object ForceInjectionContext(params object[] args)
        {
            try
            {
                var result = Aggregate((string[]) args[0], (int) args[1]);
                return result;
            }
            catch (InvalidCastException)
            {
                return null;
            }
        }
    }
}