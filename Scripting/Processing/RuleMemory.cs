using System;
using System.Collections.Generic;
using System.Linq;
using Metozis.Scripting.Injecting;

namespace Metozis.Scripting.Processing
{
    public class RuleMemory : IRuleInjective
    {
        private readonly HashSet<object> memory = new HashSet<object>();
        private readonly Dictionary<string, object> pointers = new Dictionary<string, object>();

        public RuleMemory()
        {
            ExposedMethods = new Dictionary<string, Func<object[], object>>
            {
                {"Get", args =>
                    {
                        return Get<object>((Predicate<object>)args[0]);
                    }
                },
                {"GetByPointer", args =>
                    {
                        return GetByPointer<object>((string)args[0]);
                    }
                },
                {"Put", args =>
                    {
                        Put(args[0], (string)args[1]);
                        return null;
                    }
                },
            };
        }

        public T Get<T>(Predicate<object> selectionPredicate)
        {
            return (T) memory.ToList().Find(selectionPredicate);
        }

        public T GetByPointer<T>(string pointer)
        {
            return (T) pointers[pointer];
        }

        public void Put(object o, string ptr = null)
        {
            memory.Add(o);
            if (ptr != null) pointers[ptr] = o;
        }


        public Dictionary<string, Func<object[], object>> ExposedMethods { get; }

        public object ForceInjectionContext(params object[] args)
        {
            return null;
        }
    }
}