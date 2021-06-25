using System;
using System.Collections.Generic;
using System.IO;

namespace Metozis.Scripting
{
    public abstract class ScriptReader<TData, TRule> where TRule : ScriptRule
    {
        public List<TRule> Rules { get; protected set; } = new List<TRule>();
        private readonly List<TRule> defaultRules;

        protected readonly HashSet<string> supportedExtensions = new HashSet<string>
        {
            ".txt",
            ".mscrpit"
        };
        protected HashSet<TData> cachedData = new HashSet<TData>();

        public ScriptReader(params TRule[] customRules)
        {
            if (customRules.Length > 0) Rules.AddRange(customRules);
        }

        protected void CatchReadingException()
        {
            //TODO: Implement writing exceptions to logs
        }

        protected virtual bool ValidatePath(string path)
        {
            if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                
                if (supportedExtensions.Contains(fileInfo.Extension)) return true;
            }

            return false;
        }

        public abstract void Read(string path);
        protected abstract void InitializeDefaultRules();
    }
}