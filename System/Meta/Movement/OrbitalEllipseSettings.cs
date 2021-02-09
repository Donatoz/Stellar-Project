using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Metozis.System.Meta.Movement
{
    [Serializable]
    public class OrbitalEllipseSettings : EllipseSettings
    {
        public float SemiMajorAxis = 1f;
        [Range(0, 0.95f)]
        public float Eccentricity;
        [Range(0 ,4)]
        public float Inclination;
        [Range(0 ,4)]
        public float AcendingNodeLongitude;
        public float PeriapsisArgument;
        public float MeanLongitude;
        [Range(0, 1)]
        public float ErrorRate = 0.2f;
        [Range(3, 20)]
        public int EvaluationAccuracyChanges = 5;

        public static OrbitalEllipseSettings GetRandom()
        {
            return new OrbitalEllipseSettings
            {
                SemiMajorAxis = 1,
                Eccentricity = Random.Range(0, 0.95f),
                Inclination = Random.Range(0, 4),
                AcendingNodeLongitude = Random.Range(0, 4),
                PeriapsisArgument = Random.Range(0, 100),
                MeanLongitude = Random.Range(0, 5),
                ErrorRate = Random.Range(0.1f, 0.2f),
                EvaluationAccuracyChanges = Random.Range(4, 7)
            };
        }
    }
}