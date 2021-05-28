////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto the Moon>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorMoon {

    //Colour settings reference
    ColourSettingsMoon settingsMoon;

    // 2D texture and constant int resolution for texture
    Texture2D textureMoon;
    const int MoontextureResolution = 50;

    // Biome noise filter
    INoiseFilterMoon biomeNoisefilterMoon;

    // Colour generator
    public void UpdateSettingsMoon(ColourSettingsMoon settingsMoon) {

        //this settings = settings
        this.settingsMoon = settingsMoon;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureMoon == null || textureMoon.height != settingsMoon.biomeColourSettingsMoon.biomesMoon.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureMoon = new Texture2D(MoontextureResolution * 2, settingsMoon.biomeColourSettingsMoon.biomesMoon.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterMoon = NoiseFilterFactoryMoon.CreateNoiseFilterMoon(settingsMoon.biomeColourSettingsMoon.noiseMoon);
        
    }

    // Update elevation
    public void UpdateElevationMoon(MinMaxMoon elevationMinMaxMoon) {

        // Set planet material based on the elevation of the geometry
        settingsMoon.MoonMaterial.SetVector("_elevationMinMaxMoon", new Vector4(elevationMinMaxMoon.MinMoon, elevationMinMaxMoon.MaxMoon));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointMoon(Vector3 pointOnUnitSphereMoon) {

        // Hieght percent float
        float MoonheightPercent = (pointOnUnitSphereMoon.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        MoonheightPercent += (biomeNoisefilterMoon.EvaluateMoon(pointOnUnitSphereMoon) - settingsMoon.biomeColourSettingsMoon.MoonnoiseOffset) * settingsMoon.biomeColourSettingsMoon.MoonnoiseStrength;

        // biome index = 0
        float MoonbiomeIndex = 0;

        // Number of biomes depending on the biome length
        int MoonnumBiomes = settingsMoon.biomeColourSettingsMoon.biomesMoon.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float MoonblendRange = settingsMoon.biomeColourSettingsMoon.MoonblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < MoonnumBiomes; i++) {

            // Float distance for the biome settings
            float dst = MoonheightPercent - settingsMoon.biomeColourSettingsMoon.biomesMoon[i].MoonstartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-MoonblendRange, MoonblendRange, dst);

            // Reset biome index to 0
            MoonbiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            MoonbiomeIndex += i * weight;

        }

        // return biome index
        return MoonbiomeIndex / Mathf.Max(1, MoonnumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursMoon() {

        // new colour array for texture resolution
        Color[] Mooncolours = new Color[textureMoon.width * textureMoon.height];

        // Colour index
        int MooncolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Moonbiome in settingsMoon.biomeColourSettingsMoon.biomesMoon) {

            // for loop for texture resolution
            for (int i = 0; i < MoontextureResolution * 2; i++) {

                // Colour for colour gradient
                Color MoongradientCol;

                // If i is less than the texture resolution
                if (i < MoontextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    MoongradientCol = settingsMoon.oceanColourMoon.Evaluate(i / (MoontextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    MoongradientCol = Moonbiome.Moongradient.Evaluate((i - MoontextureResolution) / (MoontextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color MoontintCol = Moonbiome.Moontint;

                // gradient colour with biome tint 
                Mooncolours[MooncolourIndex] = MoongradientCol * (1 - Moonbiome.MoontintPercent) + MoontintCol * Moonbiome.MoontintPercent;

                // Increment colour index
                MooncolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureMoon.SetPixels(Mooncolours);
        textureMoon.Apply();
        settingsMoon.MoonMaterial.SetTexture("_textureMoon", textureMoon);

    }

}