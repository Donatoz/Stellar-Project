using Metozis.System.Entities;

namespace Metozis.System.Generators.Preprocessors
{
    public interface IGeneratorPostprocessor
    {
        void Resolve(Entity entity, GenerationOptions options);
    }
}