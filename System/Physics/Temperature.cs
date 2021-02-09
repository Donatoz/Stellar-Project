using Metozis.System.Management;
using UnityEngine;

namespace Metozis.System.Physics
{
    public static class Temperature
    {
        public const float MAX_COLOR_TEMPERATURE = 60000;
        public const float TEMPERATURE_TO_RADIUS = 10000;
        
        public static Color GetTemperatureColor(float temperature)
        {
            return ManagersRoot.Get<PhysicsManager>()
                .TemperatureMap.Evaluate(Mathf.Clamp(temperature, 0, MAX_COLOR_TEMPERATURE) / MAX_COLOR_TEMPERATURE);
        }

        public static float GetRadiusFromTemperature(float temperature)
        {
            return Mathf.Clamp(temperature / TEMPERATURE_TO_RADIUS, 0, Mathf.Infinity);
        }
    }
}