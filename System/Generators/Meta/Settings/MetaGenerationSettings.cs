using Metozis.System.Meta;
using Sirenix.OdinInspector;

namespace Metozis.System.Generators.Meta
{
    public abstract class MetaGenerationSettings : ArgumentsTuple
    {
        [BoxGroup("Generation Settings")]
        public MetaNamespace Namespace;

        public virtual void RegisterProperties()
        {
            // Pass
        }
    }
}