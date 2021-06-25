using System;
using Metozis.System.Meta.Templates;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Generators.Meta
{
    [Serializable]
    public class PlanetMetaGenerationSettings : StellarBodyMetaGenerationSettings
    {
        [BoxGroup("Generation Settings")]
        public PlanetTemplate[] Templates;

        [MinMaxSlider(0, 3)]
        [BoxGroup("Generation Settings")]
        public Vector2Int ChildrenDepth;
        [MinMaxSlider(0, 5)] 
        [BoxGroup("Generation Settings")]
        public Vector2Int ChildAmountRange;
        [BoxGroup("Generation Settings")]
        public PlanetMetaGenerationSettings[] ChildrenVariants;
    }
}