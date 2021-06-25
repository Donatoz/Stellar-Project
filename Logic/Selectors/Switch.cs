using System;
using System.Collections.Generic;
using System.Linq;

namespace Metozis.Logic.Selectors
{
    public class Switch<TKey, TVal> : ISelector<TKey, TVal>
    {
        private readonly List<(TKey, TVal)> options;
        private readonly Predicate<TKey> validator;

        public Switch(Predicate<TKey> validator, params (TKey, TVal)[] options)
        {
            this.options = new List<(TKey, TVal)>();
            this.validator = validator;
            if (options != null)
            {
                this.options.AddRange(options);
            }
        }
        
        public TVal Select(TKey key)
        {
            return options.Where(tuple => validator(tuple.Item1)).First().Item2;
        }
    }
}