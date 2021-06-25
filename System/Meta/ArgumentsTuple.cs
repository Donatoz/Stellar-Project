using System;

namespace Metozis.System.Meta
{
    [Serializable]
    public abstract class ArgumentsTuple
    {
        public T As<T>() where T : ArgumentsTuple
        {
            return this as T;
        }
    }
}