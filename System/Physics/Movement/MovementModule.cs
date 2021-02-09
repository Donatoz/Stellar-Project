using Metozis.System.Entities;
using Metozis.System.Entities.Modules;
using Metozis.System.Management;
using Metozis.System.Meta;
using Metozis.System.Meta.Movement;
using Metozis.System.Reactive;
using UnityEngine;

namespace Metozis.System.Physics.Movement
{
    public class MovementModule : Module, IRuntimeModule
    {
        public ReactiveValue<IMovementController> Settings = new ReactiveValue<IMovementController>();

        private float timer;
        
        public MovementModule(Entity target) : base(target)
        {
            Settings.OnValueChanged = newSettings =>
            {
                newSettings.DrawPath();
            };
        }
        
        private void Move()
        {
            Target.transform.position = Settings.Value.GetEvaluatedPosition(ManagersRoot.Get<TimeManager>().CurrentTime);
        }

        public void FixedUpdate()
        {
            if (!Enabled || Settings == null) return;
            Settings.Value.SetCenter(Target.transform.parent);
            Move();
        }
    }
}