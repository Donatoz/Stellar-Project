using System.Text.RegularExpressions;
using Metozis.Scripting.Injecting;

namespace Metozis.Scripting
{
    public abstract class PatternRule : ScriptRule
    {
        public string Pattern;
        
        protected PatternRule(params IRuleInjective[] initialInjectives) : base(initialInjectives){}

        public override bool Validate(string line)
        {
            return Validation?.Invoke(line) ?? Regex.IsMatch(line, Pattern);
        }
    }
}