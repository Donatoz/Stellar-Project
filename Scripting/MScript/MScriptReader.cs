using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Metozis.System.IO;
using UnityEngine;

namespace Metozis.Scripting.MScript
{
    public sealed class MScriptReader : ScriptReader<Action, MScriptRule>
    {
        private StringBuilder logs = new StringBuilder();
        public MScriptRule Root { get; private set; }
        
        private int currentLine = 0;
        private int traverseDepth = 0;

        private const int MaximumDepth = 100;
        
        public MScriptReader(params MScriptRule[] customRules) : base(customRules)
        {
            InitializeDefaultRules();
        }
        
        public override void Read(string path)
        {
            logs.Clear();
            var lines = InputUtils.ReadFile(path).ToList();
            for (var i = 0; i < lines.Count; i++)
            {
                traverseDepth = 0;
                var line = lines[i];
                if (line.Trim().Length == 0)
                {
                    continue;
                }
                currentLine = i + 1;
                var correspondingRule = TraverseRule(Regex.Replace(line.Trim(), @" {2,}", " ").Split(' '), Root);
                if (correspondingRule.Item2 == null) continue;
                Debug.Log("Found rule");
                Debug.Log("Rule evaluation results:");
                foreach (var result in correspondingRule.Item2)
                {
                    Debug.Log(result);
                }
            }
            Debug.Log($"Logs:\n{logs}");
        }

        private (MScriptRule, IEnumerable<object>) TraverseRule(IReadOnlyList<string> splicedLine, MScriptRule currentRule, int idx = 0)
        {
            Debug.Log($"Depth: {traverseDepth}");
            if (currentRule.Closing) return (currentRule, currentRule.Evaluate(splicedLine[idx]));
            
            traverseDepth++;
            if (traverseDepth >= MaximumDepth)
            {
                logs.Append($"[{currentLine}]Maximum depth exceeded");
                return default;
            }
            
            currentRule.Evaluate(splicedLine[idx]);

            var next = currentRule.Select(splicedLine[idx]);
            
            if (next == null)
            {
                logs.Append($"[{currentLine}]No rule found");
                return default;
            }

            return TraverseRule(splicedLine, next, ++idx);
        }

        protected override void InitializeDefaultRules()
        {
            Rules = new List<MScriptRule>();
            Root = RuleClusters.AssembleDefaultRoot();
            Debug.Log("Assembled root");
            Debug.Log(Root);
            Rules.Add(Root);
        }
    }
}