using System;
using LeTai;
using Metozis.System.Extensions.Copy;
using Metozis.System.Generators;
using Metozis.System.Meta.Templates;
using Metozis.System.VFX;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Entities
{
    public partial class Planet : StellarObject, ILightReceiver, IFormulaic
    {
        private LightSource source;
        private MeshRenderer bodyRenderer;
        private MeshRenderer ringsRenderer;
        private MeshRenderer outerAtmosRenderer;

        public override Type GetGeneratorType()
        {
            return typeof(Planet);
        }

        protected override void Awake()
        {
            base.Awake();
            
            bodyRenderer = transform.Find("Body").GetComponent<MeshRenderer>();
            ringsRenderer = transform.Find("Rings").GetComponent<MeshRenderer>();
            outerAtmosRenderer = transform.Find("Atmosphere").GetComponent<MeshRenderer>();
        }

        public void SetLightPoint(GameObject light)
        {
            // TODO: Bring this to the effects module
            if (source == null)
            {
                source = gameObject.AddComponent<LightSource>();
            }
            source.Sun = light;
        }

        public void ApplyTemplate(Template template)
        {
            if (!(template is PlanetTemplate)) return;
            if (bodyRenderer == null)
            {
                bodyRenderer = transform.Find("Body").GetComponent<MeshRenderer>();
            }
            var planetTemplate = (PlanetTemplate) template;
            
            bodyRenderer.material.SetTexture("_ColorTexture", planetTemplate.BaseTexture);
            bodyRenderer.material.SetTexture("_Normals", planetTemplate.BaseNormal);
            bodyRenderer.material.SetFloat("_NormalsIntensity", planetTemplate.NormalIntensity);
            bodyRenderer.material.SetTexture("_EmissionTexture", planetTemplate.EmissionTexture);
            bodyRenderer.material.SetColor("_EmissionColor", planetTemplate.EmissionColor);
            bodyRenderer.material.SetKeyword("_ADDITIVEEMISSION_ON", planetTemplate.AdditiveEmission);
            bodyRenderer.material.SetFloat("_EmissionDistortionAmount", planetTemplate.EmissionDistortionAmount);
            bodyRenderer.material.SetFloat("_EmissionDistortionScale", planetTemplate.EmissionDistortionScale);
            bodyRenderer.material.SetVector("_EmissionAlphaMinMax", planetTemplate.EmissionAlphaMinMax);
            bodyRenderer.material.SetFloat("_EmissionSpeed", planetTemplate.EmissionSpeed);
            
            bodyRenderer.material.SetColor("_AtmosphereColor", planetTemplate.AtmoshpereColor);
            bodyRenderer.material.SetFloat("_InteriorSize", planetTemplate.InteriorSize);
            bodyRenderer.material.SetFloat("_InteriorIntensity", planetTemplate.InteriorIntensity);
            bodyRenderer.material.SetColor("_IlluminationAmbient", planetTemplate.IlluminationAmbient);
            bodyRenderer.material.SetFloat("_IlluminationBoost", planetTemplate.IlluminationBoost);
            bodyRenderer.material.SetColor("_SkyblendA", planetTemplate.SkyBlend);
            
            bodyRenderer.material.SetTexture("_CloudsTexture", planetTemplate.CloudsTexture);
            bodyRenderer.material.SetTexture("_CloudsNormals", planetTemplate.CloudsNormal);
            bodyRenderer.material.SetFloat("_CloudSpeed", planetTemplate.CloudSpeed);
            bodyRenderer.material.SetColor("_ColorA", planetTemplate.CloudsColor);
            bodyRenderer.material.SetFloat("_ReliefIntensity", planetTemplate.CloudsReliefIntensity);
            bodyRenderer.material.SetFloat("_ReliefSmoothness", planetTemplate.CloudsReliefSmoothness);
            bodyRenderer.material.SetColor("_ShadowColorA", planetTemplate.CloudsShadowColor);
            bodyRenderer.material.SetFloat("_ShadowsXOffset", planetTemplate.CloudsShadowOffset.x);
            bodyRenderer.material.SetFloat("_ShadowsYOffset", planetTemplate.CloudsShadowOffset.y);
            bodyRenderer.material.SetFloat("_ShadowsSharpness", planetTemplate.CloudsShadowSharpness);
            bodyRenderer.material.SetTexture("_CloudsDistortionTexture", planetTemplate.CloudDistortionTexture);
            bodyRenderer.material.SetFloat("_CloudDistortionSpeed", planetTemplate.CloudDistortionSpeed);
            bodyRenderer.material.SetFloat("_CloudDistortionAmount", planetTemplate.CloudDistortionAmount);

            bodyRenderer.material.SetColor("_Citiescolor", planetTemplate.CitiesColor);
            bodyRenderer.material.SetFloat("_CitiesDetail", planetTemplate.CitiesDetails);
            bodyRenderer.material.SetVector("_CitiesAlphaMinMax", planetTemplate.CitiesAlphaMinMax);
            bodyRenderer.material.SetVector("_CitiesAlphaTiling", planetTemplate.CitiesAlphaTiling);
            bodyRenderer.material.SetTexture("_CitiesMask", planetTemplate.CitiesMask);
            bodyRenderer.material.SetVector("_CitiesMaskMinMax", planetTemplate.CitiesMaskMinMax);
            bodyRenderer.material.SetVector("_CitiesMaskTiling", planetTemplate.CitiesMaskTiling);
            
            bodyRenderer.material.SetTexture("_NecessaryWaterMask", planetTemplate.WaterMask);
            bodyRenderer.material.SetColor("_WaterColor", planetTemplate.WaterColor);
            bodyRenderer.material.SetFloat("_SpecularIntensity", planetTemplate.WaterSpecularIntensity);
            
            bodyRenderer.material.SetTexture("_MagmaTexture", planetTemplate.MagmaTexture);
            bodyRenderer.material.SetTexture("_MagmaDistortion", planetTemplate.MagmaDistortion);
            bodyRenderer.material.SetVector("_MagmaDistortionSpeed", planetTemplate.MagmaDistortionSpeed);
            bodyRenderer.material.SetFloat("_MagmaDistortionAmount", planetTemplate.MagmaDistortionAmount);
            bodyRenderer.material.SetTexture("_MagmaMask", planetTemplate.MagmaMask);
            bodyRenderer.material.SetVector("_MagmaMaskMinMax", planetTemplate.MagmaMaskMinMax);
            bodyRenderer.material.SetVector("_MagmaMaskTiling", planetTemplate.MagmaMaskTiling);
            bodyRenderer.material.SetColor("_MagmaBgColor", planetTemplate.MagmaBgColor);
            
            bodyRenderer.material.SetFloat("_DissolveNoiseScale", planetTemplate.DissolveNoiseScale);
            bodyRenderer.material.SetFloat("_DissolveNoiseStrength", planetTemplate.DissolveNoiseStrength);
            bodyRenderer.material.SetFloat("_DissolveEdgeWidth", planetTemplate.DissolveEdgeWidth);
            bodyRenderer.material.SetColor("_DissolveColor", planetTemplate.DissolveColor);
            bodyRenderer.material.SetFloat("_DissolveProgress", planetTemplate.DissolveProgress);
            
            outerAtmosRenderer.material.SetColor("_AtmosphereColor", planetTemplate.OuterAtmosphereColor);
            outerAtmosRenderer.material.SetFloat("_ExteriorSize", planetTemplate.ExteriorSize);
            outerAtmosRenderer.material.SetFloat("_ExteriorIntensity", planetTemplate.ExteriorIntensity);
            
            ringsRenderer.material.SetTexture("_RingsTexture", planetTemplate.RingsTexture);
            ringsRenderer.material.SetFloat("_Opacity", planetTemplate.RingsOpacity);
            ringsRenderer.material.SetColor("_BaseColoRing", planetTemplate.RingsFirstColor);
            ringsRenderer.material.SetColor("_Color1", planetTemplate.RingsSecondColor);
            ringsRenderer.material.SetFloat("_Coloroffset", planetTemplate.RingsColorOffset);
            ringsRenderer.material.SetFloat("_Size", planetTemplate.RingsSize);
            ringsRenderer.material.SetFloat("_ShadowsIntensity", planetTemplate.RingsShadowIntensity);
            
            ringsRenderer.gameObject.SetActive(planetTemplate.RenderRings);
            outerAtmosRenderer.gameObject.SetActive(planetTemplate.RenderAtmosphere);
        }
    }
}