using System;
using System.Collections.Generic;
using Metozis.System.Meta;
using Metozis.System.Meta.Movement;
using Metozis.System.Meta.Templates.VFX;
using UnityEngine;

namespace Metozis.System.Generators.EntityGeneration
{
    [Serializable]
    public class StellarBodyGenerationOptions : GenerationOptions, ISealedStellarBodyGenerationOptions
    {
        [SerializeField, HideInInspector]
        private PhysicsSettings physics;
        
        
        public IMovementController MovementSettings;
        /// <summary>
        /// Must be located in Resources/Templates
        /// </summary>
        public string PathToPrefab;

        public float DistanceFromRoot = 1;
        public float Radius = 1;
        public List<EffectTemplate> AdditionalEffects = new List<EffectTemplate>();
        public bool IgnoreRelativeGravity;
        public PhysicsSettings PhysicsSettings
        {
            get => physics;
            set => physics = value;
        }
        
        public List<StellarBodyGenerationOptions> Children = new List<StellarBodyGenerationOptions>();
    }
}