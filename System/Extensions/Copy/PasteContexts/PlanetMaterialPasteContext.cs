using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using UnityEngine;

namespace Metozis.System.Extensions.Copy.PasteContexts
{
    public class PlanetMaterialPasteContext : IPasteContext
    {
        private readonly Dictionary<Type, Action<object, string, Material>> settingFunctions = new Dictionary<Type, Action<object, string, Material>>
        {
            {
                typeof(float),
                (value, paramName, mat) =>
                {
                    mat.SetFloat(paramName, (float)value);
                }
            }
        };
        
        public IEnumerable<(string, string)> GetTargetFields()
        {
            return null;
        }

        public void Resolve(object target, IReadOnlyCollection<(string, object)> values)
        {
            if (!(target is Planet)) return;
            var planet = target as Planet;
            var material = planet.transform.Find("Body").GetComponent<MeshRenderer>().sharedMaterial;

            foreach (var value in values)
            {
                settingFunctions[value.Item2.GetType()].Invoke(value.Item2, value.Item1, material);
                Debug.Log($"Pasted {value.Item2} in {value.Item1}");
            }
        }
    }
}