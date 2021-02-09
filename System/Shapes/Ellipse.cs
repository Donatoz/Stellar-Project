using System;
using Metozis.System.Meta;
using Metozis.System.Meta.Movement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Shapes
{
    public class Ellipse : Shape
    {
        [BoxGroup("Shape settings/Ellipse settings")]
        [ShowIf("showEllipseSettings")]
        public float xAxis;
        [BoxGroup("Shape settings/Ellipse settings")]
        [ShowIf("showEllipseSettings")]
        public float yAxis;
        [BoxGroup("Shape settings/Ellipse settings")]
        [ShowIf("showEllipseSettings")]
        public EllipseSettings EllipseArguments;

        protected virtual bool showEllipseSettings => true;
        
        public override Vector3 Evaluate(float t)
        {
            return default;
        }

        public override void Recalculate()
        {
            points = new Vector3[EllipseArguments.Segments + 1];
            for (int i = 0; i < EllipseArguments.Segments; i++)
            {
                var angle = (float) i / (float) EllipseArguments.Segments * 360 * Mathf.Deg2Rad;
                var x = Mathf.Sin(angle) * xAxis;
                var y = Mathf.Cos(angle) * yAxis;
                points[i] = new Vector3(x * AxisTransform.x, 0, y * AxisTransform.z);
            }

            points[EllipseArguments.Segments] = points[0];
            Renderer.SetPoints(points);
        }

        public override void SetUp(ShapeSettings settings)
        {
            if (!(settings is EllipseSettings)) return;
            EllipseArguments = (EllipseSettings)settings;
        }
    }
}