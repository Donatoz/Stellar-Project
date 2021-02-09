using UnityEngine;
using UnityEngine.VFX;
using NotImplementedException = System.NotImplementedException;

namespace Metozis.System.VFX
{
    public class VisualEffectProxy : IEffectsProxy
    {
        private VisualEffect target;
        
        public void SetTarget(Object target)
        {
            if (!(target is VisualEffect)) return;
            this.target = target as VisualEffect;
        }

        public void ChangeColor(string attribute, Color value)
        {
            target.SetVector4(attribute, value);
        }

        public void ChangeFloat(string attribute, float value)
        {
            target.SetFloat(attribute, value);
        }
    }
}