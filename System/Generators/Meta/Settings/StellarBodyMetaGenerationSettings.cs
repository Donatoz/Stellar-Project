using System;
using Metozis.System.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Generators.Meta
{
    [Serializable]
    public abstract class StellarBodyMetaGenerationSettings : MetaGenerationSettings
    {
        [BoxGroup("Generation Settings")]
        [MinMaxSlider(-273, 30000)]
        public Vector2 TemperatureRange;
        [MinMaxSlider(1, 100)]
        [BoxGroup("Generation Settings")]
        public Vector2 MassRange;
        [MinMaxSlider(0.1f, 10)] 
        [BoxGroup("Generation Settings")]
        public Vector2 SizeRange;
    }
}