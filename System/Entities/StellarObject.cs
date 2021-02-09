using System;
using Metozis.System.Entities.Modules;
using Metozis.System.Extensions;
using Metozis.System.Extensions.Copy;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Physics;
using Metozis.System.Reactive;
using Metozis.System.VFX;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace Metozis.System.Entities
{
    public abstract class StellarObject : Entity, IReproducible
    {
        /// <summary>
        /// Radius of the stellar object.
        /// When it is being changed, transform.localScale changes accordingly.
        /// </summary>
        [BoxGroup("Stellar settings")]
        public readonly ReactiveValue<float> Radius = new ReactiveValue<float> {Value = 1};
        
        /// <summary>
        /// Main stellar body.
        /// </summary>
        [BoxGroup("Stellar settings")]
        [SerializeField]
        protected GameObject mainBody;
        public PhysicsModule Physics => GetModule<PhysicsModule>();
        public EffectsModule Effects => GetModule<EffectsModule>();
        
        [BoxGroup("Stellar settings")]
        [CopyDestination("StellarObject/Meta")]
        [CopySource("StellarObject/Meta")]
        public StellarBodyGenerationOptions Meta;

        protected override void Awake()
        {
            base.Awake();

            Physics.Temperature.OnValueChanged += newTemp =>
            {
                mainBody.GetComponent<MeshRenderer>().material.SetFloat("_Temperature", newTemp);
                Effects.ChangeEffectProperty(typeof(VisualEffect), "Effect1", 
                    "Color", 
                    Temperature.GetTemperatureColor(newTemp).LerpToWhite(0.2f).HDR(2), 
                    EffectsModule.AlterOptions.Color);
            };

            Radius.OnValueChanged += newRadius =>
            {
                transform.localScale = newRadius.UniformVector();
            };
        }

        protected override void InitializeModules()
        {
            AddModule(new PhysicsModule(this));
            AddModule(new EffectsModule(this));
        }

        public abstract Type GetGeneratorType();
    }
}