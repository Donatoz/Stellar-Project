using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using Metozis.System.Entities.Modules;
using Metozis.System.Reactive;
using UnityEngine;
using UnityEngine.VFX;

namespace Metozis.System.VFX
{
    public class EffectsModule : Module
    {
        public enum AlterOptions
        {
            Float,
            Color
        }

        public readonly Dictionary<Type, Func<IEffectsProxy>> ProxyMapping = new Dictionary<Type, Func<IEffectsProxy>>
        {
            {
                typeof(Renderer),
                () => new RendererEffectsProxy()
            },
            
            {
                typeof(VisualEffect),
                () => new VisualEffectProxy()
            }
        };

        private readonly Dictionary<AlterOptions, Action<IEffectsProxy, string, dynamic>> alterFunctions =
            new Dictionary<AlterOptions, Action<IEffectsProxy, string, dynamic>>
            {
                {
                    AlterOptions.Color,
                    (effect, s, val) =>
                    {
                        effect.ChangeColor(s, val);
                    }
                },
                {
                    AlterOptions.Float,
                    (effect, s, val) =>
                    {
                        effect.ChangeFloat(s, val);
                    }
                }
            };

        /// <summary>
        /// Name of the root object of all effects.
        /// Must be the child of target entity.
        /// </summary>
        public ReactiveValue<string> EffectsRootName = new ReactiveValue<string> { Value = "Effects" };
        private Transform effectsRoot;
        
        /// <summary>
        /// Holds all cached effect proxies.
        /// </summary>
        private Dictionary<string, IEffectsProxy> effectsCache = new Dictionary<string, IEffectsProxy>();
        
        public EffectsModule(Entity target) : base(target)
        {
            effectsRoot = Target.transform.Find(EffectsRootName.Value);
            EffectsRootName.OnValueChanged += delegate(string s)
            {
                effectsRoot = Target.transform.Find(s);
            };
        }
        
        public void ChangeEffectProperty(Type componentType, string effectObjectName, string propName, object value, AlterOptions options)
        {
            if (effectsCache.ContainsKey(effectObjectName))
            {
                alterFunctions[options].Invoke(effectsCache[effectObjectName], propName, value);
            }
            else
            {
                var effectContainer = effectsRoot.Find(effectObjectName);

                if (effectContainer != null)
                {
                    var effect = effectContainer.GetComponent(componentType);
                    if (effect != null)
                    {
                        var mappedValue = ProxyMapping[componentType].Invoke();
                        if (mappedValue != null)
                        {
                            mappedValue.SetTarget(effect);
                            effectsCache[effectObjectName] = mappedValue;
                            alterFunctions[options].Invoke(mappedValue, propName, value);
                        }
                    }
                    else
                    {
                        Debug.Log($"Tried to change effect {effectObjectName} param {propName} of {Target.Name}, " +
                                  "effect is null");
                    }
                }
                else
                {
                    Debug.Log($"Tried to change effect {effectObjectName} param {propName} of {Target.Name}, " +
                              "effect container is null");
                }
            }
        }
    }
}