using System;
using System.Collections.Generic;
using Metozis.System.Extensions;
using Metozis.System.Management;
using Metozis.System.Meta;
using Metozis.System.Physics;
using Metozis.System.Reactive;
using Metozis.System.VFX;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace Metozis.System.Entities
{
    public partial class Star : StellarObject
    {
        [BoxGroup("Star settings")]
        public StarClass Class;

        /// <summary>
        /// Initialize star with given parameters.
        /// </summary>
        /// <param name="startPhysics"></param>
        public void InitializeStar(PhysicsSettings startPhysics)
        {
            Physics.SetUp(startPhysics);
        }
        
        protected override void Awake()
        {
            base.Awake();
            Physics.Temperature.OnValueChanged += temperature =>
            {
                Effects.ChangeEffectProperty(typeof(Renderer), "Crown", 
                    "_Color", 
                    Temperature.GetTemperatureColor(temperature).LerpToWhite(0.2f).HDR(2), 
                    EffectsModule.AlterOptions.Color);
            };
        }
        
        /// <summary>
        /// Can be extended in <see cref="Star"/> class extensions parts.
        /// </summary>
        partial void ExtensionStart();

        protected virtual void Start()
        {
            ExtensionStart();
        }

        public override Type GetGeneratorType()
        {
            return typeof(Star);
        }
    }
}