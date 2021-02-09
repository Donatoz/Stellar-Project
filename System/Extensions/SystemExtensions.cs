using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Metozis.System.Extensions
{
    public static class SystemExtensions
    {
        public static Vector3 UniformVector(this float f)
        {
            return new Vector3(f,f,f);
        }

        public static T PickRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static void Print<T>(this IEnumerable<T> collection, string prefix = "")
        {
            var builder = new StringBuilder();
            builder.Append(prefix);
            foreach (var obj in collection)
            {
                builder.Append(obj).Append(",");
            }
            Debug.Log(builder.ToString());
        }
    }
}