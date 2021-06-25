using System;
using System.Collections.Generic;
using Metozis.Scripting.Injecting;

namespace Metozis.Scripting
{
    public abstract class ScriptRule
    {
        public Predicate<string> Validation;
        public Func<string, IEnumerable<object>> Evaluation;
        public readonly InjectionBatcher Injection;

        public Action OnEvaluated;

        protected ScriptRule(params IRuleInjective[] initialInjectives)
        {
            Injection = new InjectionBatcher(this);
            foreach (var injective in initialInjectives)
            {
                Injection.Inject(injective);
            }
        }

        public virtual bool Validate(string line)
        {
            return Validation?.Invoke(line) ?? true;
        }

        public virtual IEnumerable<object> Evaluate(string line)
        {
            OnEvaluated?.Invoke();
            return !Validate(line) || Evaluation == null ? null : Evaluation(line);
        }
    }
}