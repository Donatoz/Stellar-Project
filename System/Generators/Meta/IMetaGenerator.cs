namespace Metozis.System.Generators.Meta
{
    public interface IMetaGenerator
    {
        GenerationOptions GenerateMeta(MetaGenerationSettings args);
    }
}