using UnityEngine;

namespace Metozis.System.Extensions
{
    public static class UnityExtensions
    {
        public static Vector3 UniformlyRandomizedBy(this Vector3 v, Vector2 value)
        {
            var randomVal = Random.Range(value.x, value.y);
            return new Vector3(v.x + randomVal, v.y + randomVal, v.z + randomVal);
        }
    }
}