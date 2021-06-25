using System;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Meta;
using Random = UnityEngine.Random;

namespace Metozis.System.Generators.Meta
{
    public static class MetaSubGenerators
    {
        public static readonly Func<
            StellarBodyGenerationOptions,
            StellarBodyMetaGenerationSettings,
            StellarBodyGenerationOptions> StellarBodySubGenerator =
            delegate(StellarBodyGenerationOptions provider, StellarBodyMetaGenerationSettings settings)
            {
                provider.Name = "Obj";
                provider.Guid = "";
                provider.Radius = Random.Range(settings.SizeRange.x, settings.SizeRange.y);
                provider.PhysicsSettings = new PhysicsSettings
                {
                    Mass = Random.Range(settings.MassRange.x, settings.MassRange.y),
                    Temperature = Random.Range(settings.TemperatureRange.x, settings.TemperatureRange.y)
                };

                return provider;
            };
    }
}