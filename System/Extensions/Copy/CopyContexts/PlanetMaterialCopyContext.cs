using System.Collections.Generic;
using Metozis.System.Entities;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Metozis.System.Extensions.Copy.CopyContexts
{
    public class PlanetMaterialCopyContext : ICopyContext
    {
        public List<(string, object)> Resolve(object target)
        {
            if (!(target is Planet)) return null;
            var obj = target as Planet;
            var material = obj.transform.Find("Body").GetComponent<MeshRenderer>().sharedMaterial;

            return new List<(string, object)>
            {
                ("_ColorTexture", material.GetTexture("_ColorTexture")),
                ("_Normals", material.GetTexture("_Normals")),
                ("_NormalsIntensity", material.GetFloat("_NormalsIntensity")),
                ("_EmissionTexture", material.GetTexture("_EmissionTexture")),
                ("_EmissionColor", material.GetColor("_EmissionColor")),
                ("_ADDITIVEEMISSION_ON", material.IsKeywordEnabled("_ADDITIVEEMISSION_ON")),
                ("_EmissionDistortionAmount", material.GetFloat("_EmissionDistortionAmount")),
                ("_EmissionDistortionScale", material.GetFloat("_EmissionDistortionScale")),
                ("_EmissionAlphaMinMax", material.GetVector("_EmissionAlphaMinMax")),
                ("_EmissionSpeed", material.GetFloat("_EmissionSpeed")),
                ("_AtmosphereColor", material.GetColor("_AtmosphereColor")),
                ("_InteriorIntensity", material.GetFloat("_InteriorIntensity")),
                ("_IlluminationAmbient", material.GetColor("_IlluminationAmbient")),
                ("_IlluminationBoost", material.GetFloat("_IlluminationBoost")),
                ("_SkyblendA", material.GetColor("_SkyblendA")),
                ("_CloudsTexture", material.GetTexture("_CloudsTexture")),
                ("_CloudsNormals", material.GetTexture("_CloudsNormals")),
                ("_CloudSpeed", material.GetFloat("_CloudSpeed")),
                ("_CloudDistortionTexture", material.GetTexture("_CloudDistortionTexture")),
                ("_CloudDistortionSpeed", material.GetVector("_CloudDistortionSpeed")),
                ("_CloudDistortionAmount", material.GetFloat("_CloudDistortionAmount")),
                ("_ColorA", material.GetColor("_ColorA")),
                ("_ReliefIntensity", material.GetFloat("_ReliefIntensity")),
                ("_ReliefSmoothness", material.GetFloat("_ReliefSmoothness")),
                ("_ShadowColorA", material.GetColor("_ShadowColorA")),
                ("_ShadowsXOffset", material.GetFloat("_ShadowsXOffset")),
                ("_ShadowsYOffset", material.GetFloat("_ShadowsYOffset")),
                ("_ShadowsSharpness", material.GetFloat("_ShadowsSharpness")),
                ("_Citiescolor", material.GetColor("_Citiescolor")),
                ("_CitiesDetail", material.GetFloat("_CitiesDetail")),
                ("_CitiesAlphaMinMax", material.GetVector("_CitiesAlphaMinMax")),
                ("_CitiesAlphaTiling", material.GetVector("_CitiesAlphaTiling")),
                ("_CitiesMask", material.GetTexture("_CitiesMask")),
                ("_CitiesMaskTiling", material.GetVector("_CitiesMaskTiling")),
                ("_CitiesMaskMinMax", material.GetVector("_CitiesMaskMinMax")),
                ("_NecessaryWaterMask", material.GetTexture("_NecessaryWaterMask")),
                ("_WaterColor", material.GetColor("_WaterColor")),
                ("_MagmaTexture", material.GetTexture("_MagmaTexture")),
                ("_MagmaDistortion", material.GetTexture("_MagmaDisotrtion")),
                ("_MagmaDistortionSpeed", material.GetVector("_MagmaDistortionSpeed")),
                ("_MagmaDistortionAmount", material.GetFloat("_MagmaDistortionAmount")),
                ("_MagmaMask", material.GetTexture("_MagmaMask")),
                ("_MagmaMaskMinMax", material.GetVector("_MagmaMaskMinMax")),
                ("_MagmaMaskTiling", material.GetVector("_MagmaMaskTiling")),
                ("_MagmaColor", material.GetColor("_MagmaColor")),
                ("_MagmaBgColor", material.GetColor("_MagmaBgColor")),
                ("_DissolveNoise", material.GetTexture("_DissolveNoise")),
                ("_DissolveNoiseStrength", material.GetFloat("_DissolveNoiseStrength")),
                ("_DissolveEdgeWidth", material.GetFloat("_DissolveEdgeWidth")),
                ("_DissolveColor", material.GetColor("_DissolveColor")),
                ("_DissolveProgress", material.GetFloat("_DissolveProgress")),
                ("_SpecularIntensity", material.GetFloat("_SpecularIntensity")),
                ("_InteriorSize", material.GetFloat("_InteriorSize"))
            };
        }
    }
}