using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metozis.System.Shapes
{
    public static class ShapeFactory
    {
        private static readonly Dictionary<Type, Func<Shape>> subFactories = new Dictionary<Type, Func<Shape>>
        {
            {
                typeof(Ellipse),
                () =>
                {
                    var newObj = new GameObject("Ellipse");
                    return newObj.AddComponent<Ellipse>();
                }
            },
            {
                typeof(OrbitalEllipse),
                () =>
                {
                    var newObj = new GameObject("Orbital Ellipse");
                    return newObj.AddComponent<OrbitalEllipse>();
                }
            }
        };

        private static readonly Dictionary<string, Type> typePointers = new Dictionary<string, Type>
        {
            {
                "Ellipse",
                typeof(Ellipse)
            },
            {
                "OrbitalEllipse",
                typeof(OrbitalEllipse)
            }
        };

        public static Type GetTypePointer(string pointer)
        {
            return typePointers[pointer];
        }
        
        public static Shape Create(Type shapeType)
        {
            return subFactories[shapeType].Invoke();
        }
    }
}