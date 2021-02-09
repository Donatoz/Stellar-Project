using System.Collections.Generic;

namespace Metozis.System.Extensions.Copy.CopyContexts
{
    public interface ICopyContext
    {
        List<(string, object)> Resolve(object target);
    }
}