using System;
using Sirenix.OdinInspector;

namespace Metozis.System.Reactive
{
    public class ReactiveValue<T>
    {
        [ShowInInspector]
        private T val;
        
        public T Value
        {
            get => val;
            set
            {
                val = value;
                OnValueChanged?.Invoke(val);
            }
        }
        
        /// <summary>
        /// Change without invoking an event.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeImperceptibly(T val)
        {
            this.val = val;
        }
        
        /// <summary>
        /// Invoked when <see cref="Value"/> is changed.
        /// </summary>
        public Action<T> OnValueChanged;
    }
}