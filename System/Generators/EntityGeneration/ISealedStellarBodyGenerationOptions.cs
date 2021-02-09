using System;
using Metozis.System.Meta;
using UnityEngine;

namespace Metozis.System.Generators.EntityGeneration
{
    public interface ISealedStellarBodyGenerationOptions
    {
        PhysicsSettings PhysicsSettings { get; set; }
    }
}