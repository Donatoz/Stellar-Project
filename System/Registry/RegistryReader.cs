using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Metozis.System.Extensions;
using Metozis.System.IO;
using Shapes;
using Sirenix.Utilities;
using UnityEngine;
using Metozis.Scripting.Processing;
using Random = UnityEngine.Random;

namespace Metozis.System.Registry
{
    public class RegistryReader
    {
        private HashSet<string> data = new HashSet<string>();
        private Func<string, RegistryRule[]> selectiveFunction;

        private List<RegistryRule> rules = new List<RegistryRule>
        {
            // Registry start/end
            new RegistryRule
            {
                Validation = line => line == "REG_START" || line == "REG_END",
                Pass = true
            },
            // Generative rule
            new RegistryRule
            {
                Pattern = @"\w+[-][*]\[\d+[-]\d+\]{{\d+[-]\d+}}",
                Evaluation = line =>
                {
                    var spliced = Regex.Split(line, @"[-][*]");
                    var prefix = spliced[0];
                    var splicedRight = spliced[1];
                    var amount = Regex.Split(splicedRight, @"\[\d+[-]\d+\]")[1]
                        .Replace("{{", "").Replace("}}", "");
                    var digitsAmount = Regex.Split(splicedRight, @"[{]{2}\d+[-]\d+[}]{2}")[0]
                        .Replace("[", "").Replace("]", "");

                    var splicedAmount = amount.Split('-');
                    Vector2 amountMinMax = new Vector2(int.Parse(splicedAmount[0]), int.Parse(splicedAmount[1]));

                    var splicedDigits = digitsAmount.Split('-');
                    Vector2 digitsMinMax = new Vector2(int.Parse(splicedDigits[0]), int.Parse(splicedDigits[1]));
                    
                    var result = new List<string>();
                    for (int i = 0; i < Random.Range(amountMinMax.x, amountMinMax.y); i++)
                    {
                        var digits = new StringBuilder();
                        for (int j = 0; j < Random.Range(digitsMinMax.x, digitsMinMax.y); j++)
                        {
                            digits.Append(Random.Range(0, 9));
                        }
                        if (digits[0] == '0')
                        {
                            digits.Remove(0, 1);
                        }
                        result.Add($"{prefix}-{digits}");
                    }

                    return result;
                }
            },
            // Comment line
            new RegistryRule
            {
                Validation = line => line.StartsWith("//"),
                Pass = true
            },
            // Namespace start rule
            new RegistryRule
            (new LineAggregator
                {
                    StopSymbol = "}",
                    SubAggregation = line => line.Replace(",", "").Trim()
                }
            )
            {
                Validation = line => line.StartsWith("{")
            }
        };

        public RegistryReader(params RegistryRule[] customRules)
        {
            if (customRules.Length > 0) rules.AddRange(customRules);
            // Primitive selective function (selects all rules).
            // TODO: Implement selection as graph search by predicate
            selectiveFunction = line => rules.ToArray();
        }
        
        public void Load(string path)
        {
            var lines = InputUtils.ReadFile(path).ToList();
            if (lines.First() != "REG_START" || lines.Last() != "REG_END")
            {
                WriteLogs("CRITICAL: Every registry script should start with REG_START and end with REG_END");
                return;
            }
            
            var logs = new StringBuilder();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.Trim().Length == 0) continue;
                
                var tempResults = new List<string>();
                var passLine = false;
                foreach (var rule in selectiveFunction.Invoke(line))
                {
                    if (rule.Validate(line) && rule.Pass)
                    {
                        passLine = true;
                        break;
                    }

                    if (rule.Injection.IsInjected<LineAggregator>())
                    {
                        if (!rule.Validate(line)) continue;
                        rule.Injection.ForceCall<LineAggregator>(lines.ToArray(), i + 1);
                        var results = rule.Injection.GetState<LineAggregator, IReadOnlyList<string>>();
                        Debug.Log(results.Count);
                        tempResults.AddRange(results);
                        i += results.Count + 1;
                    }
                    else
                    {
                        var evaluationResults = rule.Evaluate(line);
                        if (evaluationResults != null)
                        {
                            tempResults.AddRange(evaluationResults.Cast<string>());
                        }
                    }
                }

                if (tempResults.Count == 0)
                {
                    if (!passLine) logs.AppendLine($"Line[{i + 1}]: No rules found or rules gave no results.");
                }
                else
                {
                    data.AddRange(tempResults);
                }
            }
            
            if (logs.Length > 0)
            {
                WriteLogs(logs.ToString());
            }
            else
            {
                var output = new StringBuilder();
                foreach (var piece in data)
                {
                    output.AppendLine(piece);
                }
                WriteLogs("No problems found :) \nOutput:\n" + output);
            }
        }

        private void WriteLogs(string content)
        {
            OutputUtils.WriteFile(Global.PathVariables.MetozisRoot + "/Registry/Logs/logs.txt", content);
        }

        public string Get()
        {
            var item = data.PickRandom();
            data.Remove(item);
            return item;
        }
    }
}