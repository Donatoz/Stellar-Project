using System;
using System.Collections.Generic;
using Metozis.System.Generators.EntityGeneration;

namespace Metozis.System.Generators.StellarGeneration
{
    [Serializable]
    public class StellarSystemGenerationOptions : GenerationOptions
    {
        public List<StellarBodyGenerationOptions> RootSystem = new List<StellarBodyGenerationOptions>();
        public List<StellarBodyGenerationOptions> Members = new List<StellarBodyGenerationOptions>();
        /// <summary>
        /// For debug
        /// </summary>
        public List<PlanetGenerationOptions> Planets = new List<PlanetGenerationOptions>();
        /// <summary>
        /// For debug
        /// </summary>
        public List<StarGenerationOptions> Stars = new List<StarGenerationOptions>();
        public bool RootRelativeGravitation;
        public float OrbitSpace;
        public float InitialSemiMajorAxis;
        public float AdditionalDistanceFromRoot;
        public float ChildDistanceFromRoot;
    }
}