using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using Metozis.System.Generators.StellarGeneration;
using UnityEngine;

namespace Metozis.System.Stellar
{
    public class StellarSystem : Entity
    {
        public List<StellarObject> Root;
        public List<StellarObject> Members;
        public StellarSystemGenerationOptions Meta;

        public void AppendObject(StellarObject obj, StellarObject parent)
        {
            Members.Add(obj);
        }

        protected override void InitializeModules()
        {
            
        }
    }
}