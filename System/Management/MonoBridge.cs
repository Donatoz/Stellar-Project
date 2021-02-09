using System;
using UnityEngine;

namespace Metozis.System.Management
{
    public class MonoBridge : MonoBehaviour
    {
        public static MonoBehaviour Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }
    }
}