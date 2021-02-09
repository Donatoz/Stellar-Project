using System;
using Metozis.System.Management;
using Metozis.System.Meta;
using Metozis.System.Reactive;
using UnityEngine;

namespace Metozis.System.Entities.Modules
{
    public class PhysicsModule : Module
    {
        public readonly ReactiveValue<float> Mass;
        public readonly ReactiveValue<float> Temperature;
        
        public PhysicsModule(Entity target) : base(target)
        {
            Mass = new ReactiveValue<float>();
            Temperature = new ReactiveValue<float>();
        }

        public void SetUp(PhysicsSettings settings, bool silent = false)
        {
            if (silent)
            {
                Mass.ChangeImperceptibly(settings.Mass);
                Temperature.ChangeImperceptibly(settings.Temperature);
            }
            else
            {
                Mass.Value = settings.Mass;
                Temperature.Value = settings.Temperature;
            }
        }
    }
}