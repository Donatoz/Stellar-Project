using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Meta.Templates
{
    [Serializable]
    public class StarTemplate : Template
    {
        public override string TemplateRootPath => "Stars/";
        
        public Vector3 MinimumSize;
        [MinMaxSlider(300, 60000)]
        public Vector2 TemperatureRange = new Vector2(10000, 20000);

    }
}