using System;
using System.Collections.Generic;
using System.Linq;
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

        public static T PickRandom<T>(this IEnumerable<T> coll)
        {
            var enumerable = coll as T[] ?? coll.ToArray();
            return enumerable.ToList()[Random.Range(0, enumerable.Length)];
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

    public static class ExceptionAggregator
    {
        public static bool Among(Exception e, params Type[] expectedTypes)
        {
            return expectedTypes.Any(t => t == e.GetType());
        }
    }
}