using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Metozis.Scripting;
using Metozis.Scripting.Injecting;
using Metozis.Scripting.Processing;
using UnityEngine;

namespace Metozis.System.Registry
{
    public class RegistryRule : ScriptRule, IPatternRule
    {
        public string Pattern { get; set; }
        public bool Pass;

        public RegistryRule(params IRuleInjective[] initialInjectives) : base(initialInjectives) {}
        public RegistryRule(string pattern, params IRuleInjective[] initialInjectives) : base(initialInjectives)
        {
            Pattern = pattern;
        }
        
        public bool Match(string line)
        {
            return Regex.IsMatch(line, Pattern);
        }

        public override bool Validate(string line)
        {
            return Validation?.Invoke(line) ?? Pattern != null && Match(line);
        }
    }
}