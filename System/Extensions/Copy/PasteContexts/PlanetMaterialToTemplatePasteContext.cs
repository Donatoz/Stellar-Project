using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Metozis.System.Entities;
using Metozis.System.Meta.Templates;
using UnityEngine;

namespace Metozis.System.Extensions.Copy.PasteContexts
{
    public class PlanetMaterialToTemplatePasteContext : IPasteContext
    {
        public IEnumerable<(string, string)> GetTargetFields()
        {
            return null;
        }

        public void Resolve(object target, IReadOnlyCollection<(string, object)> values)
        {
            if (!(target is Planet)) return;
            var dict = new Dictionary<string, object>();
            foreach (var value in values)
            {
                if (!value.Item1.StartsWith("_")) continue;
                dict[value.Item1] = value.Item2;
            }
            
            var fields = target.GetType().GetFields(CopyBuffer.SearchFlags)
                .Where(f => f.IsDefined(typeof(CopyDestinationAttribute), false)
                && f.GetCustomAttribute<CopyDestinationAttribute>().Id == "Generation/Planet/Template");
            
            var template = new PlanetTemplate();
            template.BaseTexture = (Texture2D)dict["_ColorTexture"];
            template.BaseNormal = (Texture2D)dict["_Normals"];
            template.NormalIntensity = (float)dict["_NormalsIntensity"];
            template.EmissionTexture = (Texture2D) dict["_EmissionTexture"];
            template.EmissionColor = (Color) dict["_EmissionColor"];
            template.AdditiveEmission = (bool) dict["_ADDITIVEEMISSION_ON"];
            template.EmissionDistortionAmount = (float) dict["_EmissionDistortionAmount"];
            template.EmissionDistortionScale = (float) dict["_EmissionDistortionScale"];
            template.EmissionSpeed = (float) dict["_EmissionSpeed"];
            template.EmissionAlphaMinMax = (Vector4) dict["_EmissionAlphaMinMax"];
            template.AtmoshpereColor = (Color)dict["_AtmosphereColor"];
            template.InteriorIntensity = (float)dict["_InteriorIntensity"];
            template.InteriorSize = (float)dict["_InteriorSize"];
            template.IlluminationAmbient = (Color)dict["_IlluminationAmbient"];
            template.IlluminationBoost = (float)dict["_IlluminationBoost"];
            template.SkyBlend = (Color)dict["_SkyblendA"];
            template.CloudsTexture = (Texture2D)dict["_CloudsTexture"];
            template.CloudsNormal = (Texture2D)dict["_CloudsNormals"];
            template.CloudSpeed = (float)dict["_CloudSpeed"];
            template.CloudsColor = (Color)dict["_ColorA"];
            template.CloudsReliefIntensity = (float)dict["_ReliefIntensity"];
            template.CloudsReliefSmoothness = (float)dict["_ReliefSmoothness"];
            template.CloudsShadowColor = (Color)dict["_ShadowColorA"];
            template.CloudsShadowOffset = new Vector2((float)dict["_ShadowsXOffset"], (float)dict["_ShadowsYOffset"]);
            template.CloudsShadowSharpness = (float)dict["_ShadowsSharpness"];
            template.CitiesColor = (Color)dict["_Citiescolor"];
            template.CitiesDetails = (float)dict["_CitiesDetail"];
            template.WaterMask = (Texture2D)dict["_NecessaryWaterMask"];
            template.WaterColor = (Color)dict["_WaterColor"];
            template.WaterSpecularIntensity = (float)dict["_SpecularIntensity"];
            
            foreach (var field in fields)
            {
                field.SetValue(target, template);
            }
        }
    }
}