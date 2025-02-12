﻿using System;
using System.Linq;
using Metozis.System.Extensions;
using Metozis.System.Management;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.System.Meta.Templates
{
    [Serializable]
    public class PlanetTemplate : Template
    {
        public override string TemplateRootPath => "Planets/";
        
        [BoxGroup("Meta parameters")]
        [Range(0, 100)]
        public float BaseHabitability;
        [BoxGroup("Meta parameters")]
        public Vector3 MinimumSize = Vector3.one;
        
        [BoxGroup("Shader settings")]
        public Texture2D BaseTexture;
        [BoxGroup("Shader settings")]
        public Texture2D BaseNormal;
        [BoxGroup("Shader settings")]
        public Texture2D EmissionTexture;
        [BoxGroup("Shader settings")]
        [ColorUsage(true, true)]
        public Color EmissionColor;
        [BoxGroup("Shader settings")]
        public bool AdditiveEmission;
        [BoxGroup("Shader settings")]
        [Range(0, 1)]
        public float EmissionDistortionAmount;
        [BoxGroup("Shader settings")]
        public float EmissionDistortionScale;
        [BoxGroup("Shader settings")]
        public Vector2 EmissionAlphaMinMax;
        [BoxGroup("Shader settings")]
        public float EmissionSpeed;
        [Range(0, 2)] 
        [BoxGroup("Shader settings")]
        public float NormalIntensity;
        
        [BoxGroup("Shader settings/Atmosphere")]
        [ColorUsage(true, true)]
        public Color AtmoshpereColor;
        [BoxGroup("Shader settings/Atmosphere")] 
        [Range(0, 10)]
        public float InteriorSize;
        [BoxGroup("Shader settings/Atmosphere")]
        [Range(0, 1)]
        public float InteriorIntensity;
        [BoxGroup("Shader settings/Atmosphere")]
        public Color IlluminationAmbient;
        [BoxGroup("Shader settings/Atmosphere")]
        public float IlluminationBoost;
        [BoxGroup("Shader settings/Atmosphere")]
        public Color SkyBlend;

        [BoxGroup("Shader settings/Clouds")] 
        public Texture2D CloudsTexture;
        [BoxGroup("Shader settings/Clouds")] 
        public Texture2D CloudsNormal;
        [BoxGroup("Shader settings/Clouds")]
        [Range(0, 10)]
        public float CloudSpeed;
        [BoxGroup("Shader settings/Clouds")]
        [ColorUsage(true, true)]
        public Color CloudsColor;
        [BoxGroup("Shader settings/Clouds")]
        public float CloudsReliefIntensity;
        [BoxGroup("Shader settings/Clouds")]
        [Range(0, 5)]
        public float CloudsReliefSmoothness;
        [BoxGroup("Shader settings/Clouds")]
        public Color CloudsShadowColor;
        [BoxGroup("Shader settings/Clouds")]
        public Vector2 CloudsShadowOffset;
        [Range(0, 10)]
        [BoxGroup("Shader settings/Clouds")]
        public float CloudsShadowSharpness;
        
        [ColorUsage(true, true)]
        [BoxGroup("Shader settings/Cities")]
        public Color CitiesColor;
        [Range(1, 20)]
        [BoxGroup("Shader settings/Cities")]
        public float CitiesDetails;
        
        [BoxGroup("Shader settings/Water")]
        public Texture2D WaterMask;
        [BoxGroup("Shader settings/Water")]
        public Color WaterColor;
        [Range(0, 1)] 
        [BoxGroup("Shader settings/Water")]
        public float WaterSpecularIntensity;

        [BoxGroup("Atmosphere shader settings")]
        [ColorUsage(true, true)]
        [ShowIf("RenderAtmosphere")]
        public Color OuterAtmosphereColor;
        [BoxGroup("Atmosphere shader settings")]
        [Range(0, 1)]
        [ShowIf("RenderAtmosphere")]
        public float ExteriorSize;
        [BoxGroup("Atmosphere shader settings")]
        [Range(0, 1)]
        [ShowIf("RenderAtmosphere")]
        public float ExteriorIntensity;

        [BoxGroup("Rings shader settings")]
        [ShowIf("RenderRings")]
        public Texture2D RingsTexture;
        [BoxGroup("Rings shader settings")]
        [Range(0, 1)]
        [ShowIf("RenderRings")]
        public float RingsOpacity;
        [BoxGroup("Rings shader settings")]
        [ShowIf("RenderRings")]
        public Color RingsFirstColor;
        [ShowIf("RenderRings")]
        [BoxGroup("Rings shader settings")]
        public Color RingsSecondColor;
        [BoxGroup("Rings shader settings")]
        [ShowIf("RenderRings")]
        [Range(0, 1)]
        public float RingsColorOffset;
        [BoxGroup("Rings shader settings")]
        [ShowIf("RenderRings")]
        [Range(0, 5)]
        public float RingsSize;
        [BoxGroup("Rings shader settings")]
        [ShowIf("RenderRings")]
        [Range(0, 1)]
        public float RingsShadowIntensity;

        [BoxGroup("Render options")] 
        public bool RenderRings;
        [BoxGroup("Render options")] 
        public bool RenderAtmosphere;

        public static PlanetTemplate GetRandomFromPreferences(Func<PlanetTemplate, bool> filter = null)
        {
            return filter == null 
                ? ManagersRoot.Get<Preferences>().PlanetTemplates.PickRandom() 
                : ManagersRoot.Get<Preferences>().PlanetTemplates.Where(filter).ToList().PickRandom();
        }

        public static PlanetTemplate GetRandom()
        {
            return null;
        }
    }
}