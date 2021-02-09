using UnityEngine;

namespace Metozis.System.Extensions
{
    public static class UnityUtilities
    {
        public static Vector3 RandomPositionInDonut(Vector2 minMax)
        {
            var ratio = minMax.x / minMax.y;
            var radius = Mathf.Sqrt(Random.Range(ratio*ratio, 1f)) * minMax.y;
            return Random.insideUnitCircle.normalized * radius;
        }
    }
}