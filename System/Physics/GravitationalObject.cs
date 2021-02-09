using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using Metozis.System.Meta.Movement;
using Metozis.System.Physics.Movement;

namespace Metozis.System.Physics
{
    [Serializable]
    public class GravitationalObject : Entity
    {
        public List<string> StellarIds = new List<string>();
        public IMovementController movementController;

        private void Start()
        {
            GetModule<MovementModule>().Settings.Value = movementController;
        }

        protected override void InitializeModules()
        {
            AddModule(new MovementModule(this));
        }
    }
}