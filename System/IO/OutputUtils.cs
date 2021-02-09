using System.Collections.Generic;
using System.IO;

namespace Metozis.System.IO
{
    public static class OutputUtils
    {
        public static void WriteFile(string path, string content, bool append = false)
        {
            using (var writer = new StreamWriter(path, append))
            {
                writer.WriteLine(content);
            }
        }
    }
}