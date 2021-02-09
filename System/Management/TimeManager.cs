using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Management
{
    public class TimeManager : SerializedMonoBehaviour
    {
        public float CurrentTime { get; private set; }
        
        private void FixedUpdate()
        {
            CurrentTime += 0.03f;
        }
    }
}