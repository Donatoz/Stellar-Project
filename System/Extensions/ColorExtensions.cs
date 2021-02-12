using UnityEngine;

namespace Metozis.System.Extensions
{
    public static class ColorExtensions
    {
        public static Color HDR(this Color c, float intensity)
        {
            var factor = Mathf.Pow(2, intensity);
            return new Color(c.r * factor, c.g * factor, c.b * factor, c.a);
        }

        public static Color LerpToWhite(this Color c, float amount)
        {
            return Color.Lerp(c, Color.white, amount);
        }

        public static Color Alpha(this Color c, float a)
        {
            return new Color(c.r, c.g, c.b, a);
        }
        
        public static Color LerpToBlack(this Color c, float amount)
        {
            return Color.Lerp(c, Color.black, amount);
        }
    }
}