using System;
using System.Collections.Generic;
using Metozis.System.Entities;
using Metozis.System.Management;
using Metozis.System.Meta;
using Metozis.System.Meta.Movement;
using Metozis.System.Reactive;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Shapes
{
    public abstract class Shape : Entity
    {
        protected Vector3[] points;
        public IReadOnlyList<Vector3> Points => points;
        
        [BoxGroup("Shape settings")]
        [ShowIf("showShapeSettings")]
        public Vector3 AxisTransform = Vector3.one;
        [BoxGroup("Shape settings")]
        public ReactiveValue<Transform> Center = new ReactiveValue<Transform>();

        public bool UpdateAtRuntime;
        
        protected virtual bool showShapeSettings => true;

        public ShapeRenderModule Renderer => GetModule<ShapeRenderModule>();

        public abstract Vector3 Evaluate(float t);
        public abstract void Recalculate();

        protected override void InitializeModules()
        {
            AddModule(new ShapeRenderModule(this));
        }

        protected override void Awake()
        {
            base.Awake();
            Renderer.Initialize();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (UpdateAtRuntime)
            {
                Recalculate();
            }
        }

        public abstract void SetUp(ShapeSettings settings);
    }
}