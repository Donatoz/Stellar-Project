using System;
using Metozis.System.Entities;
using Metozis.System.Management;
using Metozis.System.Meta;
using Metozis.System.Meta.Movement;
using Metozis.System.Physics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Shapes
{
    public class OrbitalEllipse : Ellipse
    {
        [BoxGroup("Shape settings/Ellipse settings/Orbit settings")]
        public OrbitalEllipseSettings Arguments;

        protected override bool showEllipseSettings => false;
        
        public override void Recalculate()
        {
            Debug.Log(Arguments);
            var orbitFraction = 1f / Arguments.Segments;
            points = new Vector3[Arguments.Segments + 1];
            for (int i = 0; i < Arguments.Segments; i++)
            {
                float eccentricAnomaly = i * orbitFraction * (float)Constants.Tau;
                var trueAnomalyConst = Mathf.Sqrt((1 + Arguments.Eccentricity) / (1 - Arguments.Eccentricity));
                var trueAnomaly = 2 * Mathf.Atan(trueAnomalyConst * Mathf.Tan(eccentricAnomaly / 2));
                var distance = Arguments.SemiMajorAxis * (1 - Arguments.Eccentricity * Mathf.Cos(eccentricAnomaly)) + Arguments.AdditionalVisualRadius;
                
                var cosAOPPlusA = Mathf.Cos(Arguments.PeriapsisArgument + trueAnomaly);
                var sinAOPPlusA = Mathf.Sin(Arguments.PeriapsisArgument + trueAnomaly);
                var cosLOAN = Mathf.Cos(Arguments.AcendingNodeLongitude);
                var sinLOAN = Mathf.Sin(Arguments.AcendingNodeLongitude);
                var cosI = Mathf.Cos(Arguments.Inclination);
                var sinI = Mathf.Sin(Arguments.Inclination);

                var x = distance * ((cosLOAN * cosAOPPlusA) - (sinLOAN * sinAOPPlusA * cosI));
                var z = distance * ((sinLOAN * cosAOPPlusA) + (cosLOAN * sinAOPPlusA * cosI));
                var y = distance * (sinI * sinAOPPlusA);
                
                points[i] = Center.Value.position + new Vector3(x * Arguments.AxisTransform.x, y * Arguments.AxisTransform.y, z * Arguments.AxisTransform.z);
            }

            points[Arguments.Segments] = points[0];
            Renderer.SetPoints(points);
        }

        public override Vector3 Evaluate(float t)
        {
            Recalculate();
            float DF(float E, float e)
            {
                return -1f + e * Mathf.Cos(E);
            }

            float F(float E, float e, float M)
            {
                return M - E + e * Mathf.Sin(E);
            }
            
            var mu = Constants.G * Arguments.EvaluationSpeed;
            var n = Mathf.Sqrt((float)mu / Mathf.Pow(Arguments.SemiMajorAxis, 3));
            var meanAnomaly = n * (t - Arguments.MeanLongitude) + (Arguments.EvaluationProgress * 6);

            var diff = 1f;
            var E1 = meanAnomaly;
            
            for (int i = 0; diff > Arguments.ErrorRate && i < Arguments.EvaluationAccuracyChanges; i++)
            {
                var E0 = E1;
                E1 = E0 - F(E0, Arguments.Eccentricity, meanAnomaly) / DF(E0, Arguments.Eccentricity);
                diff = Mathf.Abs(E1 - E0);
            }

            var eccentricAnomaly = E1;

            var trueAnomalyConst = Mathf.Sqrt((1 + Arguments.Eccentricity) / (1 - Arguments.Eccentricity));
            var trueAnomaly = 2 * Mathf.Atan(trueAnomalyConst * Mathf.Tan(eccentricAnomaly / 2));
            var distance = Arguments.SemiMajorAxis * (1 - Arguments.Eccentricity * Mathf.Cos(eccentricAnomaly));

            var cosAOPPlusA = Mathf.Cos(Arguments.PeriapsisArgument + trueAnomaly);
            var sinAOPPlusA = Mathf.Sin(Arguments.PeriapsisArgument + trueAnomaly);
            var cosLOAN = Mathf.Cos(Arguments.AcendingNodeLongitude);
            var sinLOAN = Mathf.Sin(Arguments.AcendingNodeLongitude);
            var cosI = Mathf.Cos(Arguments.Inclination);
            var sinI = Mathf.Sin(Arguments.Inclination);

            var x = distance * ((cosLOAN * cosAOPPlusA) - (sinLOAN * sinAOPPlusA * cosI));
            var z = distance * ((sinLOAN * cosAOPPlusA) + (cosLOAN * sinAOPPlusA * cosI));
            var y = distance * (sinI * sinAOPPlusA);
            
            return Center.Value.position + new Vector3(x * Arguments.AxisTransform.x, y * Arguments.AxisTransform.y, z * Arguments.AxisTransform.z);
        }

        private void Start()
        {
            if (Center.Value == null)
            {
                Center.Value = transform;
            }
        }

        public override void SetUp(ShapeSettings settings)
        {
           if (!(settings is OrbitalEllipseSettings)) return;
           Arguments = (OrbitalEllipseSettings) settings;
        }
    }
}