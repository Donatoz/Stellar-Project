using System;
using Metozis.System.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace Metozis.System.Meta.Templates.VFX
{
    [Serializable]
    public class AsteroidBeltTemplate : EffectTemplate
    {
        [BoxGroup("Asteroid belt settings")] 
        public float Spread = 0.3f;
        [BoxGroup("Asteroid belt settings")]
        public Mesh AsteroidMesh;
        [BoxGroup("Asteroid belt settings")]
        public Texture2D AsteroidTexture;
        [BoxGroup("Asteroid belt settings")]
        [ColorUsage(true, true)]
        public Color AsteroidTint;
        [BoxGroup("Asteroid belt settings")]
        [Range(0, 6.28f)]
        public float Arc = 6.28f;
        [BoxGroup("Asteroid belt settings")]
        public Vector2 SpawnMinMax = new Vector2(0.5f, 3);
        [BoxGroup("Asteroid belt settings")]
        public float Density = 20;
        [BoxGroup("Asteroid belt settings")]
        public Vector2 SizeMinMax = new Vector2(0.04f, 0.09f);
        [BoxGroup("Asteroid belt settings")]
        public Vector3 RotationAxis = new Vector3(0, 0, 1);
        [BoxGroup("Asteroid belt settings")]
        public float RotationSpeed = 6;
        [BoxGroup("Asteroid belt settings")]
        public Vector2 LifetimeMinMax = new Vector2(10, 15);
        [BoxGroup("Asteroid belt settings")]
        public AnimationCurve SizeOverLifetime;
        
        [NonSerialized]
        public Vector3 LightSoruce;

        public override void Apply(VisualEffect effect)
        {
            effect.SetFloat("Spread", Spread);
            effect.SetMesh("Asteroid Mesh", AsteroidMesh);
            effect.SetTexture("Asteroid Texture", AsteroidTexture);
            effect.SetVector4("Asteroid Color", AsteroidTint);
            effect.SetFloat("Arc", Arc);
            effect.SetVector2("Spawn MinMax", SpawnMinMax);
            effect.SetFloat("Density", Density);
            effect.SetVector2("Size MinMax", SizeMinMax);
            effect.SetVector3("Rotation Axis", RotationAxis);
            effect.SetFloat("Rotation Speed", RotationSpeed);
            effect.SetVector2("Lifetime MinMax", LifetimeMinMax);
            effect.SetAnimationCurve("Size Over Lifetime", SizeOverLifetime);
            effect.SetVector3("Light Source", LightSoruce + new Vector3(0.01f, 0, 0));
            effect.SetVector3("Bounds", (SpawnMinMax.y * 2.5f).UniformVector());
        }
    }
}