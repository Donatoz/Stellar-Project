using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Metozis.System.VFX
{
    public class RendererEffectsProxy : IEffectsProxy
    {
        private Renderer target;
        
        public void SetTarget(Object target)
        {
            if (!(target is Renderer)) return;
            this.target = target as Renderer;
        }

        public void ChangeColor(string attribute, Color value)
        {
            target.material.SetColor(attribute, value);
        }

        public void ChangeFloat(string attribute, float value)
        {
            target.material.SetFloat(attribute, value);
        }
    }
}