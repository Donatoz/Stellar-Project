using System;
using UnityEngine;

namespace Metozis.System.Generators.StellarGeneration
{
    [Serializable]
    public class GalaxyGenerationOptions : GenerationOptions
    {
        public Vector2 RadiusMinMax;
        public int StellarsAmount;
        public float Height;
        public float AdditionalConnectionRadius;
        public float Density;
    }
}