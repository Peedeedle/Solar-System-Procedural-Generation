////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorJupiter.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto Jupiter>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorJupiter
{

    //Colour settings reference
    ColourSettingsJupiter settingsJupiter;

    // 2D texture and constant int resolution for texture
    Texture2D textureJupiter;
    const int JupitertextureResolution = 50;

    // Biome noise filter
    INoiseFilterJupiter biomeNoisefilterJupiter;

    // Colour generator
    public void UpdateSettingsJupiter(ColourSettingsJupiter settingsJupiter) {

        //this settings = settings
        this.settingsJupiter = settingsJupiter;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureJupiter == null || textureJupiter.height != settingsJupiter.biomeColourSettingsJupiter.biomesJupiter.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureJupiter = new Texture2D(JupitertextureResolution * 2, settingsJupiter.biomeColourSettingsJupiter.biomesJupiter.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterJupiter = NoiseFilterFactoryJupiter.CreateNoiseFilterJupiter(settingsJupiter.biomeColourSettingsJupiter.noiseJupiter);
        
    }

    // Update elevation
    public void UpdateElevationJupiter(MinMaxJupiter elevationMinMaxJupiter) {

        // Set planet material based on the elevation of the geometry
        settingsJupiter.JupiterMaterial.SetVector("_elevationMinMaxJupiter", new Vector4(elevationMinMaxJupiter.MinJupiter, elevationMinMaxJupiter.MaxJupiter));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointJupiter(Vector3 pointOnUnitSphereJupiter) {

        // Hieght percent float
        float JupiterheightPercent = (pointOnUnitSphereJupiter.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        JupiterheightPercent += (biomeNoisefilterJupiter.EvaluateJupiter(pointOnUnitSphereJupiter) - settingsJupiter.biomeColourSettingsJupiter.JupiternoiseOffset) * settingsJupiter.biomeColourSettingsJupiter.JupiternoiseStrength;

        // biome index = 0
        float JupiterbiomeIndex = 0;

        // Number of biomes depending on the biome length
        int JupiternumBiomes = settingsJupiter.biomeColourSettingsJupiter.biomesJupiter.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float JupiterblendRange = settingsJupiter.biomeColourSettingsJupiter.JupiterblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < JupiternumBiomes; i++) {

            // Float distance for the biome settings
            float dst = JupiterheightPercent - settingsJupiter.biomeColourSettingsJupiter.biomesJupiter[i].JupiterstartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-JupiterblendRange, JupiterblendRange, dst);

            // Reset biome index to 0
            JupiterbiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            JupiterbiomeIndex += i * weight;

        }

        // return biome index
        return JupiterbiomeIndex / Mathf.Max(1, JupiternumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursJupiter() {

        // new colour array for texture resolution
        Color[] Jupitercolours = new Color[textureJupiter.width * textureJupiter.height];

        // Colour index
        int JupitercolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Jupiterbiome in settingsJupiter.biomeColourSettingsJupiter.biomesJupiter) {

            // for loop for texture resolution
            for (int i = 0; i < JupitertextureResolution * 2; i++) {

                // Colour for colour gradient
                Color JupitergradientCol;

                // If i is less than the texture resolution
                if (i < JupitertextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    JupitergradientCol = settingsJupiter.oceanColourJupiter.Evaluate(i / (JupitertextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    JupitergradientCol = Jupiterbiome.Jupitergradient.Evaluate((i - JupitertextureResolution) / (JupitertextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color JupitertintCol = Jupiterbiome.Jupitertint;

                // gradient colour with biome tint 
                Jupitercolours[JupitercolourIndex] = JupitergradientCol * (1 - Jupiterbiome.JupitertintPercent) + JupitertintCol * Jupiterbiome.JupitertintPercent;

                // Increment colour index
                JupitercolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureJupiter.SetPixels(Jupitercolours);
        textureJupiter.Apply();
        settingsJupiter.JupiterMaterial.SetTexture("_textureJupiter", textureJupiter);

    }

}