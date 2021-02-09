using UnityEngine;
using Object = UnityEngine.Object;

namespace Metozis.System.VFX
{
    public interface IEffectsProxy
    {
        void SetTarget(Object target);
        void ChangeColor(string attribute, Color value);
        void ChangeFloat(string attribute, float value);
    }
}