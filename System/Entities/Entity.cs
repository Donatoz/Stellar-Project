using System;
using System.Collections.Generic;
using System.Linq;
using Metozis.System.Entities.Modules;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Metozis.System.Entities
{
    public abstract class Entity : SerializedMonoBehaviour
    {
        /// <summary>
        /// Global unique id
        /// </summary>
        [BoxGroup("Entity settings")]
        [ShowInInspector]
        public string Guid { get; private set; }
        /// <summary>
        /// Entity name.
        /// </summary>
        [BoxGroup("Entity settings")]
        public string Name;
        
        /// <summary>
        /// Entity extensions
        /// </summary>
        [ShowInInspector]
        [BoxGroup("Entity settings")]
        private readonly HashSet<Module> modules = new HashSet<Module>();
        private readonly Dictionary<Type, Module> modulesCache = new Dictionary<Type, Module>();
        private readonly HashSet<IRuntimeModule> runtimeModules = new HashSet<IRuntimeModule>();

        protected virtual void Awake()
        {
            Guid = EntityManager.RegisterEntity(this);
            InitializeModules();
        }

        /// <summary>
        /// Add already generated module to this entity.
        ///
        /// | Complexity: O(1)
        /// </summary>
        /// <param name="module"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Module AddModule<T>(T module) where T : Module
        {
            modules.Add(module);
            modulesCache[typeof(T)] = module;

            if (module is IRuntimeModule runtimeModule)
            {
                runtimeModules.Add(runtimeModule);
            }
            
            return module;
        }

        protected virtual void FixedUpdate()
        {
            foreach (var module in runtimeModules)
            {
                module.FixedUpdate();
            }
        }

        protected void OnDestroy()
        {
            EntityManager.UnregisterEntity(Guid);
        }

        /// <summary>
        /// Initialize all necessary modules.
        /// </summary>
        protected abstract void InitializeModules();
        
        /// <summary>
        /// Get the first module of type <see cref="T"/>
        ///
        /// Complexity: O(1)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModule<T>() where T : Module
        {
            return modulesCache.ContainsKey(typeof(T)) ? (T)modulesCache[typeof(T)] : null;
        }
    }
}