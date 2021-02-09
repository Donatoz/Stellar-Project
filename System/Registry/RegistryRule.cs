using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Metozis.System.Registry
{
    public struct RegistryRule
    {
        public string Pattern;
        public Predicate<string> Validation;
        public Func<string, IEnumerable<string>> Evaluation;
        public bool Pass;
        public bool Aggregate;
        public RuleAggregator Aggregator;

        public bool Validate(string line)
        {
            return Validation?.Invoke(line) ?? Regex.IsMatch(line, Pattern);
        }

        [CanBeNull]
        public IEnumerable<string> Evaluate(string line)
        {
            return !Validate(line) || Evaluation == null ? null : Evaluation(line);
        }
    }
}