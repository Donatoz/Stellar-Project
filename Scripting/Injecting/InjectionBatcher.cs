using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

namespace Metozis.Scripting.Injecting
{
    public class InjectionBatcher
    {
        public readonly HashSet<IRuleInjective> injectives = new HashSet<IRuleInjective>();
        private readonly ScriptRule rule;
        private readonly Dictionary<Type, object> injectivesStates = new Dictionary<Type, object>();

        public InjectionBatcher(ScriptRule rule)
        {
            this.rule = rule;
        }

        public void Inject(IRuleInjective injective)
        {
            injectives.Add(injective);
        }

        public bool IsInjected<T>() where T : IRuleInjective
        {
            var result =  injectives.Any(i => i.GetType() == typeof(T));
            Debug.Log($"Injection check: {typeof(T)} = {result}");
            return result;
        }

        public void ForceCall<T>(params object[] args)
        {
            var injective = injectives.Where(i => i.GetType() == typeof(T)).First();
            if (injective != null)
            {
                injectivesStates[typeof(T)] = injective.ForceInjectionContext(args);
            }
        }
        
        public TRet GetState<TInj, TRet>() where TInj : IRuleInjective
        {
            try
            {
                var result = (TRet) injectivesStates[typeof(TInj)];
                return result;
            }
            catch (InvalidCastException)
            {
                return default;
            }
        } 
    }
}