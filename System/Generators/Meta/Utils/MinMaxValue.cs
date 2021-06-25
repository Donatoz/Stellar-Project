using System;
using Sirenix.OdinInspector;

namespace Metozis.System.Generators.Meta.Utils
{
    [Serializable]
    public struct MinMaxValue01<T>
    {
        [MinMaxSlider(0, 1)] 
        public T Value;
    }
    
    [Serializable]
    public struct MinMaxValueTiny<T>
    {
        [MinMaxSlider(0, 10)] 
        public T Value;
    }
    
    [Serializable]
    public struct MinMaxValueSmall<T>
    {
        [MinMaxSlider(0, 100)] 
        public T Value;
    }
    
    [Serializable]
    public struct MinMaxValueBig<T>
    {
        [MinMaxSlider(0, 1000)] 
        public T Value;
    }
    
    [Serializable]
    public struct MinMaxValueLarge<T>
    {
        [MinMaxSlider(0, 10000)] 
        public T Value;
    }
}