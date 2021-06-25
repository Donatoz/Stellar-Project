using System;
using Sirenix.Utilities;

namespace Metozis.System.Generators.Meta.Utils
{
    [Serializable]
    public class LabeledProperty<T>
    {
        public T Value;
        public T Fallback;
        public string Label;
        public string[] ExcludedPropertiesLabels;

        public void Register()
        {
            if (Label.IsNullOrWhitespace()) return;
            foreach (var label in ExcludedPropertiesLabels)
            {
                MetaGenerationManager.ExcludedProperties.Add(label);
            }
        }

        public T GetEffectiveValue()
        {
            return MetaGenerationManager.IsExcluded(Label) ? Fallback : Value;
        }
    }
}