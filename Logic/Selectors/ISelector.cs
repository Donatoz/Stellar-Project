using System;

namespace Metozis.Logic.Selectors
{
    public interface ISelector<TKey, TVal>
    {
        TVal Select(TKey key);
    }
}