using System;

namespace Metozis.System.Entities.Modules
{
    public abstract class Module
    {
        public readonly Entity Target;
        protected bool Enabled { get; set; } = true;

        public Module(Entity target)
        {
            Target = target;
        }
    }
}