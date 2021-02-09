using Metozis.System.Entities;
using Metozis.System.Entities.Modules;
using Metozis.System.Management;
using Metozis.System.Reactive;
using UnityEngine;

namespace Metozis.System.Shapes
{
    public class ShapeRenderModule : Module
    {
        private LineRenderer renderer;
        private Shape shape;

        public ReactiveValue<float> Width;
        public ReactiveValue<Color> Color;
        
        public ShapeRenderModule(Entity target) : base(target)
        {
            shape = target as Shape;
            
            var lr = new GameObject("Renderer");
            lr.transform.parent = target.transform;
            renderer = lr.AddComponent<LineRenderer>();

            Width = new ReactiveValue<float>
            {
                OnValueChanged = width =>
                {
                    renderer.startWidth = renderer.endWidth = width;
                }
            };
            
            Color = new ReactiveValue<Color>
            {
                OnValueChanged = color =>
                {
                    renderer.material.SetColor("_Color", color);
                }
            };
        }

        public void ClearPoints()
        {
            renderer.positionCount = 0;
            renderer.SetPositions(new Vector3[0]);
        }

        public void Initialize()
        {
            renderer.material = ManagersRoot.Get<Preferences>().OrbitMaterial;
            renderer.textureMode = LineTextureMode.Tile;
            Width.Value = 0.03f;
            Color.Value = ManagersRoot.Get<Preferences>().OrbitDefaultColor;
        }

        public void SetPoints(Vector3[] points)
        {
            renderer.positionCount = points.Length;
            renderer.SetPositions(points);
        }
    }
}