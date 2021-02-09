using System;
using Metozis.System.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Entities
{
    public partial class Star
    {
        [BoxGroup("Star settings/Debug")]
        public bool DebugMode;
        
        [BoxGroup("Star settings/Debug")]
        [ShowIf("DebugMode")]
        public PhysicsSettings testPhysics;

        partial void ExtensionStart()
        {
            if (DebugMode)
            {
                InitializeStar(testPhysics);
            }
        }
        
        [InfoBox("Temperatire is received from testPhysics")]
        [Button(ButtonSizes.Large, ButtonStyle.Box)]
        [ShowIf("DebugMode")]
        [BoxGroup("Star settings/Debug")]
        private void ChangeTemperature()
        {
            if (mainBody)
            {
                mainBody.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Temperature", testPhysics.Temperature);
            }
        }
    }
}