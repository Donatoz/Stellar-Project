using System;
using System.Collections.Generic;

namespace Metozis.Scripting.Injecting
{
    public interface IRuleInjective
    {
        Dictionary<string, Func<object[], object>> ExposedMethods { get; }
        object ForceInjectionContext(params object[] args);
    }
}