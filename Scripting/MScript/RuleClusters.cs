using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Metozis.Logic.Selectors;
using Metozis.Scripting.MScript.Utils;
using Metozis.Scripting.Processing;
using UnityEngine;

namespace Metozis.Scripting.MScript
{
    public static class RuleClusters
    {
        public const string PrimitiveTypes = "int|float|string|bool";

        private static List<string> compiledTypes = new List<string>();
        
        public static MScriptRule AssembleDefaultRoot()
        {
            var root = new MScriptRule(null)
            {
                Pattern = "",
                Selection = StaticRuleSelectors.PatternMatcher
            };

            var commentRule = new MScriptRule(root)
            {
                Pattern = @"//.*",
                Closing = true
            };

            #region Delcaring_Branch

            var declaringRule = new MScriptRule(root)
            {
                Pattern = $@"({PrimitiveTypes})",
                Selection = (rule, linePart) =>
                {
                    // Line parts: PRIMITIVE_TYPE (0) (...)
                    // Where (0) possibly may be: NAME
                    return rule.Children[0];
                }
            };

            var nameDeclaringRule = new MScriptRule(declaringRule, declaringRule.Pattern)
            {
                Pattern = @"\w+",
                Selection = (rule, linePart) =>
                {
                    // Line parts: PRIMITIVE_TYPE NAME (0) (...)
                    // Where (0) possibly may be: assignment
                    return rule.Children[0];
                }
            };

            var assignmentRule = new MScriptRule(nameDeclaringRule, nameDeclaringRule.Pattern)
            {
                Pattern = @"=",
                Selection = (rule, linePart) =>
                {
                    // Line parts: PRIMITIVE_TYPE NAME = (0)
                    // Where (0) possibly may be: assignment
                    var nextSwitch = new Switch<string, int>(s => Regex.IsMatch(linePart, s),
                        (@"\d+", 0), // Integer
                        (@"(true|false)", 1), // Boolean
                        (@"\d+[.]\d+", 2), // Float
                        ("\".+\"", 3) // String
                        );
                    return rule.Children[nextSwitch.Select(linePart)];
                }
            };

            #region Primitive_Declarations

            var intDeclaringRule = new MScriptRule(assignmentRule, assignmentRule.Pattern, new RuleMemory())
            {
                Pattern = @"\d+",
                Closing = true
            };
            intDeclaringRule.Evaluation = s =>
            {
                var res = !int.TryParse(s, out var result) ? null : new[] {(object) result};
                Debug.Log((int)res[0]);
                return res;
            };

            var stringDeclaringRule = new MScriptRule(assignmentRule, assignmentRule.Pattern, new RuleMemory())
            {
                Pattern = "\".+\"",
                Closing = true
            };
            stringDeclaringRule.Evaluation = s =>
            {
                var res = new[] {(object) s.Replace('\"', char.MinValue)};
                Debug.Log(res[0]);
                return res;
            };
            
            #endregion
            
            

            #endregion

            return root;
        }
    }
}