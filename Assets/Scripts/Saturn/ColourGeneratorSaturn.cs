////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto Saturn>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorSaturn {

    //Colour settings reference
    ColourSettingsSaturn settingsSaturn;

    // 2D texture and constant int resolution for texture
    Texture2D textureSaturn;
    const int SaturntextureResolution = 50;

    // Biome noise filter
    INoiseFilterSaturn biomeNoisefilterSaturn;

    // Colour generator
    public void UpdateSettingsSaturn(ColourSettingsSaturn settingsSaturn) {

        //this settings = settings
        this.settingsSaturn = settingsSaturn;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureSaturn == null || textureSaturn.height != settingsSaturn.biomeColourSettingsSaturn.biomesSaturn.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureSaturn = new Texture2D(SaturntextureResolution * 2, settingsSaturn.biomeColourSettingsSaturn.biomesSaturn.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterSaturn = NoiseFilterFactorySaturn.CreateNoiseFilterSaturn(settingsSaturn.biomeColourSettingsSaturn.noiseSaturn);
        
    }

    // Update elevation
    public void UpdateElevationSaturn(MinMaxSaturn elevationMinMaxSaturn) {

        // Set planet material based on the elevation of the geometry
        settingsSaturn.SaturnMaterial.SetVector("_elevationMinMaxSaturn", new Vector4(elevationMinMaxSaturn.MinSaturn, elevationMinMaxSaturn.MaxSaturn));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointSaturn(Vector3 pointOnUnitSphereSaturn) {

        // Hieght percent float
        float SaturnheightPercent = (pointOnUnitSphereSaturn.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        SaturnheightPercent += (biomeNoisefilterSaturn.EvaluateSaturn(pointOnUnitSphereSaturn) - settingsSaturn.biomeColourSettingsSaturn.SaturnnoiseOffset) * settingsSaturn.biomeColourSettingsSaturn.SaturnnoiseStrength;

        // biome index = 0
        float SaturnbiomeIndex = 0;

        // Number of biomes depending on the biome length
        int SaturnnumBiomes = settingsSaturn.biomeColourSettingsSaturn.biomesSaturn.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float SaturnblendRange = settingsSaturn.biomeColourSettingsSaturn.SaturnblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < SaturnnumBiomes; i++) {

            // Float distance for the biome settings
            float dst = SaturnheightPercent - settingsSaturn.biomeColourSettingsSaturn.biomesSaturn[i].SaturnstartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-SaturnblendRange, SaturnblendRange, dst);

            // Reset biome index to 0
            SaturnbiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            SaturnbiomeIndex += i * weight;

        }

        // return biome index
        return SaturnbiomeIndex / Mathf.Max(1, SaturnnumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursSaturn() {

        // new colour array for texture resolution
        Color[] Saturncolours = new Color[textureSaturn.width * textureSaturn.height];

        // Colour index
        int SaturncolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Saturnbiome in settingsSaturn.biomeColourSettingsSaturn.biomesSaturn) {

            // for loop for texture resolution
            for (int i = 0; i < SaturntextureResolution * 2; i++) {

                // Colour for colour gradient
                Color SaturngradientCol;

                // If i is less than the texture resolution
                if (i < SaturntextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    SaturngradientCol = settingsSaturn.oceanColourSaturn.Evaluate(i / (SaturntextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    SaturngradientCol = Saturnbiome.Saturngradient.Evaluate((i - SaturntextureResolution) / (SaturntextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color SaturntintCol = Saturnbiome.Saturntint;

                // gradient colour with biome tint 
                Saturncolours[SaturncolourIndex] = SaturngradientCol * (1 - Saturnbiome.SaturntintPercent) + SaturntintCol * Saturnbiome.SaturntintPercent;

                // Increment colour index
                SaturncolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureSaturn.SetPixels(Saturncolours);
        textureSaturn.Apply();
        settingsSaturn.SaturnMaterial.SetTexture("_textureSaturn", textureSaturn);

    }

}