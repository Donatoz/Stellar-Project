using System;

namespace Metozis.Scripting.Injecting
{
    public interface IRuleInjective
    {
        object ForceInjectionContext(params object[] args);
    }
}