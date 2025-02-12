﻿using System;
using UnityEngine;

namespace Metozis.System.Meta.Movement
{
    [Serializable]
    public abstract class ShapeSettings : ArgumentsTuple
    {
        public Vector3 AxisTransform = Vector3.one;
        public float EvaluationSpeed = 1;
    }
}