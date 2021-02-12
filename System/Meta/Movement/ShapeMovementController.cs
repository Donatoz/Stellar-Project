using System;
using Metozis.System.Entities;
using Metozis.System.Reactive;
using Metozis.System.Shapes;
using Shapes;
using UnityEngine;

namespace Metozis.System.Meta.Movement
{
    /// <summary>
    /// Workflow:
    /// 1: Set arguments
    /// 2: Draw Path
    /// 3: Set center
    /// </summary>
    [Serializable]
    public class ShapeMovementController : IMovementController
    {
        public SerializableSystemType ShapeType;
        [SerializeField]
        private ShapeSettings args;
        
        [NonSerialized]
        public ReactiveValue<Transform> Center = new ReactiveValue<Transform>();
        [NonSerialized]
        private GameObject currentShapeContainer;

        public ShapeMovementController()
        {
            Center.OnValueChanged = transform =>
            {
                if (shape != null)
                {
                    shape.Center.Value = transform;
                    currentShapeContainer.transform.parent = transform;
                    RecalculateShape();
                }
            };
        }
        
        public ArgumentsTuple Arguments
        {
            get => args;
            set
            {
                if (!(value is ShapeSettings)) return;
                args = (ShapeSettings) value;
            }
        }

        private Shape shape;

        private void RecalculateShape()
        {
            if (shape != null)
            {
                shape.Recalculate();
            }
        }

        public void DrawPath()
        {
            if (!(ShapeType.SystemType != typeof(Shape)))
            {
                Debug.Log("Given type is not a Shape");
                return;
            }
            currentShapeContainer = new GameObject("Shape container");
            shape = currentShapeContainer.AddComponent(ShapeType.SystemType).GetComponent<Shape>();
            shape.SetUp(args);
        }

        public void HidePath()
        {
            if (shape != null)
            {
                shape.Renderer.ClearPoints();
            }
        }

        public void ChangePathVisual(float width, Color color)
        {
            if (width != default)
            {
                shape.Renderer.Width.Value = width;
            }
            if (color != default)
            {
                shape.Renderer.Color.Value = color;
            }
        }

        public void SetCenter(Transform center)
        {
            Center.Value = center;
        }

        public Vector3 GetEvaluatedPosition(float time)
        {
            return args != null && shape != null ? shape.Evaluate(time) : Vector3.zero;
        }
    }
}