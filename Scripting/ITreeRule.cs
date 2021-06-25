using System;
using System.Collections.Generic;
using Metozis.Logic.Selectors;

namespace Metozis.Scripting
{
    public interface ITreeRule<T> : ISelector<string, T>
    {
        T Parent { get; }
        List<T> Children { get; set; }
        Func<T, string, T> Selection { get; set; }
    }
}