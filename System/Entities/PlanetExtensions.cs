using Metozis.System.Extensions.Copy;
using Metozis.System.Meta.Templates;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Entities
{
    public partial class Planet
    {
        [BoxGroup("Template test")]
        [CopySource("Generation/Planet/Template")]
        [CopyDestination("Generation/Planet/Template")]
        public PlanetTemplate TestTemplate;
        
        [BoxGroup("Template test")]
        [Button("Apply test template")]
        public void ApplyTestTemplate()
        {
            ApplyTemplate(TestTemplate);
        }
    }
}