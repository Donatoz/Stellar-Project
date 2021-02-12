using System.Collections.Generic;
using Metozis.System.Meta.Templates;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Management
{
    public class Preferences : SerializedMonoBehaviour
    {
        public Material OrbitMaterial;
        [ColorUsage(true, true)]
        public Color OrbitDefaultColor;
        [ColorUsage(true, true)]
        public Color RootBorderColor;

        public List<PlanetTemplate> PlanetTemplates;
        public List<StarTemplate> StarTemplates;
    }
}