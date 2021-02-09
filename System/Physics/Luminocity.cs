using UnityEngine;

namespace Metozis.System.Physics
{
    public static class Luminocity
    {
        public static float CalculateForStar(float temperature)
        {
            var radius = Temperature.GetRadiusFromTemperature(temperature);
            return (float)(4 * Constants.Pi * radius * radius * Constants.StefanBoltzmann * Mathf.Pow(temperature, 4));
        }
    }
}