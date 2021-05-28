////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto the Neptune>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorNeptune {

    //Colour settings reference
    ColourSettingsNeptune settingsNeptune;

    // 2D texture and constant int resolution for texture
    Texture2D textureNeptune;
    const int NeptunetextureResolution = 50;

    // Biome noise filter
    INoiseFilterNeptune biomeNoisefilterNeptune;

    // Colour generator
    public void UpdateSettingsNeptune(ColourSettingsNeptune settingsNeptune) {

        //this settings = settings
        this.settingsNeptune = settingsNeptune;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureNeptune == null || textureNeptune.height != settingsNeptune.biomeColourSettingsNeptune.biomesNeptune.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureNeptune = new Texture2D(NeptunetextureResolution * 2, settingsNeptune.biomeColourSettingsNeptune.biomesNeptune.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterNeptune = NoiseFilterFactoryNeptune.CreateNoiseFilterNeptune(settingsNeptune.biomeColourSettingsNeptune.noiseNeptune);
        
    }

    // Update elevation
    public void UpdateElevationNeptune(MinMaxNeptune elevationMinMaxNeptune) {

        // Set planet material based on the elevation of the geometry
        settingsNeptune.NeptuneMaterial.SetVector("_elevationMinMaxNeptune", new Vector4(elevationMinMaxNeptune.MinNeptune, elevationMinMaxNeptune.MaxNeptune));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointNeptune(Vector3 pointOnUnitSphereNeptune) {

        // Hieght percent float
        float NeptuneheightPercent = (pointOnUnitSphereNeptune.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        NeptuneheightPercent += (biomeNoisefilterNeptune.EvaluateNeptune(pointOnUnitSphereNeptune) - settingsNeptune.biomeColourSettingsNeptune.NeptunenoiseOffset) * settingsNeptune.biomeColourSettingsNeptune.NeptunenoiseStrength;

        // biome index = 0
        float NeptunebiomeIndex = 0;

        // Number of biomes depending on the biome length
        int NeptunenumBiomes = settingsNeptune.biomeColourSettingsNeptune.biomesNeptune.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float NeptuneblendRange = settingsNeptune.biomeColourSettingsNeptune.NeptuneblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < NeptunenumBiomes; i++) {

            // Float distance for the biome settings
            float dst = NeptuneheightPercent - settingsNeptune.biomeColourSettingsNeptune.biomesNeptune[i].NeptunestartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-NeptuneblendRange, NeptuneblendRange, dst);

            // Reset biome index to 0
            NeptunebiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            NeptunebiomeIndex += i * weight;

        }

        // return biome index
        return NeptunebiomeIndex / Mathf.Max(1, NeptunenumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursNeptune() {

        // new colour array for texture resolution
        Color[] Neptunecolours = new Color[textureNeptune.width * textureNeptune.height];

        // Colour index
        int NeptunecolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Neptunebiome in settingsNeptune.biomeColourSettingsNeptune.biomesNeptune) {

            // for loop for texture resolution
            for (int i = 0; i < NeptunetextureResolution * 2; i++) {

                // Colour for colour gradient
                Color NeptunegradientCol;

                // If i is less than the texture resolution
                if (i < NeptunetextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    NeptunegradientCol = settingsNeptune.oceanColourNeptune.Evaluate(i / (NeptunetextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    NeptunegradientCol = Neptunebiome.Neptunegradient.Evaluate((i - NeptunetextureResolution) / (NeptunetextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color NeptunetintCol = Neptunebiome.Neptunetint;

                // gradient colour with biome tint 
                Neptunecolours[NeptunecolourIndex] = NeptunegradientCol * (1 - Neptunebiome.NeptunetintPercent) + NeptunetintCol * Neptunebiome.NeptunetintPercent;

                // Increment colour index
                NeptunecolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureNeptune.SetPixels(Neptunecolours);
        textureNeptune.Apply();
        settingsNeptune.NeptuneMaterial.SetTexture("_textureNeptune", textureNeptune);

    }

}