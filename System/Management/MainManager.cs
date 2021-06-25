using System.Collections.Generic;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Generators.StellarGeneration;
using Metozis.System.Stellar;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Management
{
    public partial class MainManager : SerializedMonoBehaviour
    {
        public List<Galaxy> Galaxies = new List<Galaxy>();
        public Transform WorldCenter;
        
        
        partial void Debug();
        
        private void Start()
        {
            Debug();
        }
    }
}