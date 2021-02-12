using System.Security.Policy;
using Metozis.System.Entities;
using Metozis.System.Generators.EntityGeneration;
using Metozis.System.Management;
using UnityEngine;
using UnityEngine.VFX;

namespace Metozis.System.Generators.Preprocessors
{
    public class StellarObjectPostprocessor : IGeneratorPostprocessor
    {
        public void Resolve(Entity entity, GenerationOptions options)
        {
            if(!(options is StellarBodyGenerationOptions)) return;
            var genOptions = options as StellarBodyGenerationOptions;
            var effectsRoot = new GameObject("WorldEffects").transform;
            effectsRoot.parent = entity.transform;

            foreach (var effect in genOptions.AdditionalEffects)
            {
                var templ = ManagersRoot.Get<SpawnManager>().LoadResource<GameObject>("Templates/" + effect.PathToPrefab);
                var eff = ManagersRoot.Get<SpawnManager>()
                    .InstantiateObject(templ, Vector3.zero, Quaternion.Euler(effect.Rotation)).GetComponent<VisualEffect>();
                eff.transform.parent = effectsRoot;
                eff.transform.localPosition = effect.LocalPosition;
                if (effect.Deattachable)
                {
                    eff.name += "_world";
                }
                effect.Apply(eff);
            }
        }
    }
}