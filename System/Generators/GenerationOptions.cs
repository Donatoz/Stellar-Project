using System;
using Metozis.System.Extensions.Copy;

namespace Metozis.System.Generators
{
    [Serializable]
    public class GenerationOptions
    {
        [CopySource("Generation/Name")]
        public string Name;
        [CopySource("Generation/Guid")]
        public string Guid;
    }
}