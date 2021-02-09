using System;

namespace Metozis.System.Extensions.Copy
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CopyDestinationAttribute : Attribute
    {
        public string Id;
        
        public CopyDestinationAttribute(string id)
        {
            Id = id;
        }
    }
}