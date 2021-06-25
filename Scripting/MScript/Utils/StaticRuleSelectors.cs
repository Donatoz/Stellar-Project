using System;
using System.Linq;

namespace Metozis.Scripting.MScript.Utils
{
    public static class StaticRuleSelectors
    {
        public static Func<MScriptRule, string, MScriptRule> PatternMatcher = (rule, line) =>
        {
            return rule.Children.Where(r => rule.Match(line)).First();
        };
    }
}