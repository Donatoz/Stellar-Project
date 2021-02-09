using Metozis.System.Entities;
using Metozis.System.Generators.Preprocessors;
using Metozis.System.Management;
using UnityEngine;

namespace Metozis.System.Generators
{
    public abstract class EntityGenerator
    {
        protected abstract string TemplateName { get; }

        protected GameObject template;

        public void Initialize()
        {
            template = ManagersRoot.Get<SpawnManager>().LoadResource<GameObject>($"Templates/{TemplateName}");
        }

        protected virtual bool Validate<T>(GenerationOptions options) where T : GenerationOptions
        {
            return template != null && options.GetType() == typeof(T);
        }

        public abstract Entity Generate(GenerationOptions options, IGeneratorPostprocessor postprocessor = null);
    }
}