using UnityEngine;

namespace Metozis.System.Extensions
{
    public static class UnityExtensions
    {
        public static Color HDR(this Color c, float intensity)
        {
            var factor = Mathf.Pow(2, intensity);
            return new Color(c.r * factor, c.g * factor, c.b * factor, c.a);
        }

        public static Color LerpToWhite(this Color c, float amount)
        {
            return Color.Lerp(c, Color.white, amount);
        }

        public static Vector3 UniformlyRandomizedBy(this Vector3 v, Vector2 value)
        {
            var randomVal = Random.Range(value.x, value.y);
            return new Vector3(v.x + randomVal, v.y + randomVal, v.z + randomVal);
        }
    }
}