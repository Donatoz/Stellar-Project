using System.Runtime.Serialization;
using Metozis.System.Entities;
using UnityEngine;

namespace Metozis.System.Meta.Movement
{
    public interface IMovementController
    {
        ArgumentsTuple Arguments { get; set; }
        Vector3 GetEvaluatedPosition(float time);
        void DrawPath();
        void HidePath();
        void ChangePathVisual(float width, Color color);
        void SetCenter(Transform center);
    }
}