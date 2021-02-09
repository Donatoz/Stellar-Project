using UnityEngine;

namespace Metozis.System.DataHandling
{
    public static class Probability
    {
        public static bool Roll(float chance)
        {
            return Random.value <= chance;
        }
    }
}