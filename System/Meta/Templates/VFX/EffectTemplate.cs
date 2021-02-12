using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace Metozis.System.Meta.Templates.VFX
{
    public abstract class EffectTemplate
    {
        [BoxGroup("Effect settings")]
        public string PathToPrefab;
        [BoxGroup("Effect settings")]
        public Vector3 LocalPosition;
        [BoxGroup("Effect settings")]
        public Vector3 Rotation;
        [BoxGroup("Effect settings")]
        public float AdditionalRootSize;
        [BoxGroup("Effect settings")]
        public bool Deattachable;
        public abstract void Apply(VisualEffect effect);
    }
}