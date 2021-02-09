using System;
using Metozis.System.Meta;
using Metozis.System.Physics;
using Random = UnityEngine.Random;

namespace Metozis.System.Generators.EntityGeneration
{
    [Serializable]
    public class StarGenerationOptions : StellarBodyGenerationOptions
    {
        public static StarGenerationOptions GetRandom()
        {
            return new StarGenerationOptions
            {
                Name = "Star name from catalog",
                Radius = Random.Range(0.5f, 3),
                PhysicsSettings = new PhysicsSettings
                {
                    Mass = Random.Range(0, 4000),
                    Temperature = Random.Range(1000, Temperature.MAX_COLOR_TEMPERATURE)
                }
            };
        }
    }
}