using System.Collections.Generic;

namespace Metozis.System.Extensions.Copy.PasteContexts
{
    public interface IPasteContext
    {
        /// <summary>
        /// First tuple param - field name.
        /// Second tuple param - cached value name.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IEnumerable<(string, string)> GetTargetFields();
        
        void Resolve(object target, IReadOnlyCollection<(string, object)> values);
    }
}