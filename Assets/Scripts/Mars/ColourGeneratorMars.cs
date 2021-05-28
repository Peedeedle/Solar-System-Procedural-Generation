////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto Mars>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorMars {

    //Colour settings reference
    ColourSettingsMars settingsMars;

    // 2D texture and constant int resolution for texture
    Texture2D textureMars;
    const int MarstextureResolution = 50;

    // Biome noise filter
    INoiseFilterMars biomeNoisefilterMars;

    // Colour generator
    public void UpdateSettingsMars(ColourSettingsMars settingsMars) {

        //this settings = settings
        this.settingsMars = settingsMars;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureMars == null || textureMars.height != settingsMars.biomeColourSettingsMars.biomesMars.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureMars = new Texture2D(MarstextureResolution * 2, settingsMars.biomeColourSettingsMars.biomesMars.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterMars = NoiseFilterFactoryMars.CreateNoiseFilterMars(settingsMars.biomeColourSettingsMars.noiseMars);
        
    }

    // Update elevation
    public void UpdateElevationMars(MinMaxMars elevationMinMaxMars) {

        // Set planet material based on the elevation of the geometry
        settingsMars.MarsMaterial.SetVector("_elevationMinMaxMars", new Vector4(elevationMinMaxMars.MinMars, elevationMinMaxMars.MaxMars));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointMars(Vector3 pointOnUnitSphereMars) {

        // Hieght percent float
        float MarsheightPercent = (pointOnUnitSphereMars.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        MarsheightPercent += (biomeNoisefilterMars.EvaluateMars(pointOnUnitSphereMars) - settingsMars.biomeColourSettingsMars.MarsnoiseOffset) * settingsMars.biomeColourSettingsMars.MarsnoiseStrength;

        // biome index = 0
        float MarsbiomeIndex = 0;

        // Number of biomes depending on the biome length
        int MarsnumBiomes = settingsMars.biomeColourSettingsMars.biomesMars.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float MarsblendRange = settingsMars.biomeColourSettingsMars.MarsblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < MarsnumBiomes; i++) {

            // Float distance for the biome settings
            float dst = MarsheightPercent - settingsMars.biomeColourSettingsMars.biomesMars[i].MarsstartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-MarsblendRange, MarsblendRange, dst);

            // Reset biome index to 0
            MarsbiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            MarsbiomeIndex += i * weight;

        }

        // return biome index
        return MarsbiomeIndex / Mathf.Max(1, MarsnumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursMars() {

        // new colour array for texture resolution
        Color[] Marscolours = new Color[textureMars.width * textureMars.height];

        // Colour index
        int MarscolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Marsbiome in settingsMars.biomeColourSettingsMars.biomesMars) {

            // for loop for texture resolution
            for (int i = 0; i < MarstextureResolution * 2; i++) {

                // Colour for colour gradient
                Color MarsgradientCol;

                // If i is less than the texture resolution
                if (i < MarstextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    MarsgradientCol = settingsMars.oceanColourMars.Evaluate(i / (MarstextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    MarsgradientCol = Marsbiome.Marsgradient.Evaluate((i - MarstextureResolution) / (MarstextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color MarstintCol = Marsbiome.Marstint;

                // gradient colour with biome tint 
                Marscolours[MarscolourIndex] = MarsgradientCol * (1 - Marsbiome.MarstintPercent) + MarstintCol * Marsbiome.MarstintPercent;

                // Increment colour index
                MarscolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureMars.SetPixels(Marscolours);
        textureMars.Apply();
        settingsMars.MarsMaterial.SetTexture("_textureMars", textureMars);

    }

}