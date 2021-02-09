using System.Collections.Generic;
using NotImplementedException = System.NotImplementedException;

namespace Metozis.System.Extensions.Copy.PasteContexts
{
    public class StellarBodyMetaPasteContext : IPasteContext
    {
        public IEnumerable<(string, string)> GetTargetFields()
        {
            return new List<(string, string)>
            {
                ("Name", "Generation/Name"),
                ("Guid", "Generation/Guid")
            };
        }

        public void Resolve(object target, IReadOnlyCollection<(string, object)> values)
        {
            throw new NotImplementedException();
        }
    }
}