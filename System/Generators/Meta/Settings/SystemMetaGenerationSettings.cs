using System;
using System.Linq;
using Metozis.System.Generators.Meta.Utils;
using Metozis.System.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Generators.Meta
{
    [Serializable]
    public class SystemMetaGenerationSettings : MetaGenerationSettings
    {
        [BoxGroup("Generation Settings")]
        public StarClass[] AvailableClasses;
        
        [MinMaxSlider(1, 4)]
        [Title("System settings", TitleAlignment = TitleAlignments.Centered, Bold = true, Subtitle = "System basic settings")]
        [BoxGroup("Generation Settings/System")]
        public Vector2Int RootSize;
        [MinMaxSlider(1, 10)]
        [BoxGroup("Generation Settings/System")]
        public Vector2Int MembersAmount;
        [MinMaxSlider(1, 10)]
        [BoxGroup("Generation Settings/System")]
        public Vector2 AdditionalDistanceFromRoot;
        
        [Title("Generation variants", TitleAlignment = TitleAlignments.Centered, Bold = true, Subtitle = "Randomized variants with weights")]
        [BoxGroup("Generation Settings/System/Variants")]
        public WeightedList<MinMaxValueTiny<Vector2>> InitialSemiMajorAxisVariants;
        [BoxGroup("Generation Settings/System/Variants")]
        public WeightedList<MinMaxValueTiny<Vector2>> OrbitSpaceVariants;
        [BoxGroup("Generation Settings/System/Variants")]
        public WeightedList<MinMaxValueTiny<Vector2>> RootOrbitSpaceMultiplierVariants;
        [BoxGroup("Generation Settings/System/Variants")]
        public WeightedList<MinMaxValue01<Vector2>> RootEvaluationSpeedVariants;
        
        [BoxGroup("Generation Settings/System/Variants")]
        public WeightedList<StarMetaGenerationSettings> StarGenerationVariants;
        [BoxGroup("Generation Settings/System/Variants")]
        public WeightedList<PlanetMetaGenerationSettings> PlanetGenerationVariants;
        
        [Title("System diversity", TitleAlignment = TitleAlignments.Centered, Bold = true, Subtitle = "System values evaluation over specific value")]
        [BoxGroup("Generation Settings/System/Curves")]
        public Curve RootSemiMajorAxisOverDistance;
        [BoxGroup("Generation Settings/System/Curves")]
        public Curve RootObjectRadiusOverDistance;
        [BoxGroup("Generation Settings/System/Curves")]
        public Curve MembersInclinationOverDistance;
        [BoxGroup("Generation Settings/System/Curves")]
        public Curve OrbitSpaceMulOverDistance;

        public override void RegisterProperties()
        {
            var fields = typeof(SystemMetaGenerationSettings).GetFields();
            foreach (var field in fields)
            {
                Debug.Log(field.FieldType);
            }
        }
        
        [Button]
        public void Test()
        {
            RegisterProperties();
        }
    }
}