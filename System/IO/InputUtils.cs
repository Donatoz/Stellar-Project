using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Metozis.System.IO
{
    public static class InputUtils
    {
        public static IEnumerable<string> ReadFile(string path, bool removeDuplicates = false)
        {
            if (!File.Exists(path)) return null;
            
            var lines = new List<string>();
            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return removeDuplicates ? (IEnumerable<string>)new HashSet<string>(lines) : lines;
        }
    }
}