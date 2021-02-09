using System;
using Sirenix.OdinInspector;

namespace Metozis.System.Meta.Templates
{
    [Serializable]
    public abstract class Template
    {
        public abstract string TemplateRootPath { get; }
        
        public string TemplateName;
    }
}