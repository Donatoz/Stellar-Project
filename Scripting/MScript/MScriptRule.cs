using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Metozis.Scripting.Injecting;
using UnityEngine;

namespace Metozis.Scripting.MScript
{
    public class MScriptRule : ScriptRule, ITreeRule<MScriptRule>, IPatternRule
    {
        public MScriptRule Parent { get; }
        public List<MScriptRule> Children { get; set; } = new List<MScriptRule>();
        public string Pattern { get; set; }
        public Func<MScriptRule, string, MScriptRule> Selection { get; set; }
        public bool Closing;

        public MScriptRule(
            MScriptRule parent, 
            string ParentPattern = "",
            params IRuleInjective[] initialInejctives) : base(initialInejctives)
        {
            Parent = parent;
            Pattern = ParentPattern != null ? ParentPattern + @"\s*" + Pattern : Pattern;
            parent?.Children.Add(this);
        }

        public bool Match(string line)
        {
            return Regex.IsMatch(line, Pattern);
        }

        public MScriptRule Select(string linePart)
        {
            return Validate(linePart) ?  Children != null ? Selection(this, linePart.Trim()) : null : null;
        }
    }
}