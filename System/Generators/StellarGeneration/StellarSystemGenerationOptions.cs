using System;
using System.Collections.Generic;
using Metozis.System.Generators.EntityGeneration;

namespace Metozis.System.Generators.StellarGeneration
{
    [Serializable]
    public class StellarSystemGenerationOptions : GenerationOptions
    {
        public List<ISealedStellarBodyGenerationOptions> RootSystem = new List<ISealedStellarBodyGenerationOptions>();
        public List<StellarBodyGenerationOptions> Members = new List<StellarBodyGenerationOptions>();
        public bool RootRelativeGravitation;
        public float OrbitSpace;
        public float InitialSemiMajorAxis;
        public float AdditionalDistanceFromRoot;
        public float ChildDistanceFromRoot;
    }
}