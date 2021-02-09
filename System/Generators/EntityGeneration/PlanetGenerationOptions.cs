using System;
using Metozis.System.Extensions.Copy;
using Metozis.System.Meta.Templates;

namespace Metozis.System.Generators.EntityGeneration
{
    [Serializable]
    public class PlanetGenerationOptions : StellarBodyGenerationOptions
    {
        public bool EnableTemplate;
        [CopySource("Generation/Planet/Template")]
        public PlanetTemplate Template;
    }
}