using System;
using UnityEngine;

namespace Metozis.System.Generators.Meta.Utils
{
    [Serializable]
    public class Curve
    {
        public AnimationCurve AnimCurve;
        public float Multiplier = 1;

        public float Evaluate(float t)
        {
            return AnimCurve.Evaluate(t) * Multiplier;
        }
    }
}