using System;
using Metozis.Scripting;
using Metozis.Scripting.Injecting;
using Metozis.Scripting.Processing;
using UnityEngine;

namespace Metozis.System.Registry
{
    public class RegistryRule : PatternRule
    {
        public bool Pass;

        public RegistryRule(params IRuleInjective[] initialInjectives) : base(initialInjectives) {}
    }
}