using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Management
{
    public class ManagersRoot : MonoBehaviour
    {
        private static Dictionary<Type, MonoBehaviour> managersCache = new Dictionary<Type, MonoBehaviour>();

        private void Awake()
        {
            managersCache[typeof(MainManager)] = GetComponent<MainManager>();
            managersCache[typeof(TimeManager)] = GetComponent<TimeManager>();
            managersCache[typeof(PhysicsManager)] = GetComponent<PhysicsManager>();
            managersCache[typeof(SpawnManager)] = GetComponent<SpawnManager>();
            managersCache[typeof(Preferences)] = GetComponent<Preferences>();
        }

        public static T Get<T>() where T : MonoBehaviour
        {
            return (T)managersCache[typeof(T)];
        }
    }
}