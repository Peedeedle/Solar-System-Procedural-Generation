////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorMercury.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto Mercury>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorMercury {

    //Colour settings reference
    ColourSettingsMercury settingsMercury;

    // 2D texture and constant int resolution for texture
    Texture2D textureMercury;
    const int MercurytextureResolution = 50;

    // Biome noise filter
    INoiseFilterMercury biomeNoisefilterMercury;

    // Colour generator
    public void UpdateSettingsMercury(ColourSettingsMercury settingsMercury) {

        //this settings = settings
        this.settingsMercury = settingsMercury;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureMercury == null || textureMercury.height != settingsMercury.biomeColourSettingsMercury.biomesMercury.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureMercury = new Texture2D(MercurytextureResolution * 2, settingsMercury.biomeColourSettingsMercury.biomesMercury.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterMercury = NoiseFilterFactoryMercury.CreateNoiseFilterMercury(settingsMercury.biomeColourSettingsMercury.noiseMercury);
        
    }

    // Update elevation
    public void UpdateElevationMercury(MinMaxMercury elevationMinMaxMercury) {

        // Set planet material based on the elevation of the geometry
        settingsMercury.MercuryMaterial.SetVector("_elevationMinMaxMercury", new Vector4(elevationMinMaxMercury.MinMercury, elevationMinMaxMercury.MaxMercury));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointMercury(Vector3 pointOnUnitSphereMercury) {

        // Hieght percent float
        float MercuryheightPercent = (pointOnUnitSphereMercury.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        MercuryheightPercent += (biomeNoisefilterMercury.EvaluateMercury(pointOnUnitSphereMercury) - settingsMercury.biomeColourSettingsMercury.MercurynoiseOffset) * settingsMercury.biomeColourSettingsMercury.MercurynoiseStrength;

        // biome index = 0
        float MercurybiomeIndex = 0;

        // Number of biomes depending on the biome length
        int MercurynumBiomes = settingsMercury.biomeColourSettingsMercury.biomesMercury.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float MercuryblendRange = settingsMercury.biomeColourSettingsMercury.MercuryblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < MercurynumBiomes; i++) {

            // Float distance for the biome settings
            float dst = MercuryheightPercent - settingsMercury.biomeColourSettingsMercury.biomesMercury[i].MercurystartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-MercuryblendRange, MercuryblendRange, dst);

            // Reset biome index to 0
            MercurybiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            MercurybiomeIndex += i * weight;

        }

        // return biome index
        return MercurybiomeIndex / Mathf.Max(1, MercurynumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursMercury() {

        // new colour array for texture resolution
        Color[] Mercurycolours = new Color[textureMercury.width * textureMercury.height];

        // Colour index
        int MercurycolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Mercurybiome in settingsMercury.biomeColourSettingsMercury.biomesMercury) {

            // for loop for texture resolution
            for (int i = 0; i < MercurytextureResolution * 2; i++) {

                // Colour for colour gradient
                Color MercurygradientCol;

                // If i is less than the texture resolution
                if (i < MercurytextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    MercurygradientCol = settingsMercury.oceanColourMercury.Evaluate(i / (MercurytextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    MercurygradientCol = Mercurybiome.Mercurygradient.Evaluate((i - MercurytextureResolution) / (MercurytextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color MercurytintCol = Mercurybiome.Mercurytint;

                // gradient colour with biome tint 
                Mercurycolours[MercurycolourIndex] = MercurygradientCol * (1 - Mercurybiome.MercurytintPercent) + MercurytintCol * Mercurybiome.MercurytintPercent;

                // Increment colour index
                MercurycolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureMercury.SetPixels(Mercurycolours);
        textureMercury.Apply();
        settingsMercury.MercuryMaterial.SetTexture("_textureMercury", textureMercury);

    }

}