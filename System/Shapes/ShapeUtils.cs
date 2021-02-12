using Metozis.System.Meta.Movement;
using UnityEngine;

namespace Metozis.System.Shapes
{
    public static class ShapeUtils
    {
        public static Ellipse DrawEllipse(float x, float y, Vector3 position)
        {
            var ellipse = new GameObject("Ellipse").AddComponent<Ellipse>();
            ellipse.transform.position = position;
            ellipse.EllipseArguments = new EllipseSettings();
            ellipse.EllipseArguments.Segments = 80;
            ellipse.xAxis = x;
            ellipse.yAxis = y;
            ellipse.Recalculate();
            return ellipse;
        }
    }
}