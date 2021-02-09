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
                ("_ColorA", material.GetColor("_ColorA")),
                ("_ReliefIntensity", material.GetFloat("_ReliefIntensity")),
                ("_ReliefSmoothness", material.GetFloat("_ReliefSmoothness")),
                ("_ShadowColorA", material.GetColor("_ShadowColorA")),
                ("_ShadowsXOffset", material.GetFloat("_ShadowsXOffset")),
                ("_ShadowsYOffset", material.GetFloat("_ShadowsYOffset")),
                ("_ShadowsSharpness", material.GetFloat("_ShadowsSharpness")),
                ("_Citiescolor", material.GetColor("_Citiescolor")),
                ("_CitiesDetail", material.GetFloat("_CitiesDetail")),
                ("_NecessaryWaterMask", material.GetTexture("_NecessaryWaterMask")),
                ("_WaterColor", material.GetColor("_WaterColor")),
                ("_SpecularIntensity", material.GetFloat("_SpecularIntensity")),
                ("_InteriorSize", material.GetFloat("_InteriorSize"))
            };
        }
    }
}