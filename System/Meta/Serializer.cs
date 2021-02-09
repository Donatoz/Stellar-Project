using Metozis.System.Entities;
using Newtonsoft.Json;

namespace Metozis.System.Meta
{
    public static class Serializer
    {
        public static string SerializeObject(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}