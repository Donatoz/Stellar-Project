using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Metozis.System.Extensions.Copy.CopyContexts;
using Metozis.System.Extensions.Copy.PasteContexts;
using UnityEngine;

namespace Metozis.System.Extensions.Copy
{
    public static class CopyBuffer
    {
        private static readonly Dictionary<string, object> cache = new Dictionary<string, object>();

        public static readonly BindingFlags SearchFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static void Clear()
        {
            cache.Clear();
        }
        
        public static void Set(string key, object value)
        {
            cache[key] = value;
        }

        public static object Get(string key)
        {
            return cache[key];
        }

        public static void ReadFields(object target)
        {
            Clear();
            var fields = target.GetType().GetFields(SearchFlags)
                .Where(f => f.IsDefined(typeof(CopySourceAttribute), false));
            foreach (var field in fields)
            {
                var info = (field.GetCustomAttribute<CopySourceAttribute>().Id, field.GetValue(target));
                Set(info.Id, info.Item2);
                Debug.Log($"Copied field: {info.Id} = {info.Item2}");
            }
        }

        public static void PasteFields(object target)
        {
            var fields = target.GetType().GetFields(SearchFlags)
                .Where(f => f.IsDefined(typeof(CopyDestinationAttribute), false));
            
            foreach (var field in fields)
            {
                var value = Get(field.GetCustomAttribute<CopySourceAttribute>().Id);
                field.SetValue(target, value);
                Debug.Log($"Pasted field: {field.Name} = {value}");
            }
        }

        public static void MakeCopy(object target, ICopyContext context)
        {
            var data = context.Resolve(target);
            if (data == null) return;
            Clear();
            
            foreach (var tuple in data)
            {
                Set(tuple.Item1, tuple.Item2);
                Debug.Log($"Copied: {tuple.Item1} = {tuple.Item2}");
            }
        }

        private static List<(string, object)> GetBoxedCache()
        {
            return cache.Select(kvp => (kvp.Key, kvp.Value)).ToList();
        }
        
        public static void PasteCopy(object target, IPasteContext context)
        {
            var fields = context.GetTargetFields();

            if (fields != null)
            {
                var targetType = target.GetType();

                foreach (var field in fields)
                {
                    try
                    {
                        targetType.GetField(field.Item1).SetValue(target, Get(field.Item2));
                        Debug.Log($"Pasted data in {field.Item1}");
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
            }
            else
            {
                try
                {
                    context.Resolve(target, GetBoxedCache());
                }
                catch (NotImplementedException)
                {
                    Debug.Log("Paste failed.");
                }
            }
        }
    }
}