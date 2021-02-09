using System;

namespace Metozis.System.Extensions.Copy
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CopySourceAttribute : Attribute
    {
        public string Id;
        
        public CopySourceAttribute(string id)
        {
            Id = id;
        }
    }
}