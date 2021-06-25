using System;
using System.Collections.Generic;
using DataStructures.RandomSelector;

namespace Metozis.System.Generators.Meta.Utils
{
    [Serializable]
    public class WeightedList<T>
    {
        [Serializable]
        public struct WeightedItem<T>
        {
            public T Value;
            public float Weight;
        }

        public List<WeightedItem<T>> items = new List<WeightedItem<T>>();

        public T PickRandom()
        {
            var picker = new DynamicRandomSelector<T>(-1, items.Count);
            foreach (var item in items)
            {
                picker.Add(item.Value, item.Weight);
            }

            picker.Build();
            return picker.SelectRandomItem();
        }
    }
}