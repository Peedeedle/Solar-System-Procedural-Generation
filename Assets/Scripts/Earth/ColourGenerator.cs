////////////////////////////////////////////////////////////
// File:                 <ColourGenerator.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto the planet>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGenerator {

    //Colour settings reference
    ColourSettings settings;

    // 2D texture and constant int resolution for texture
    Texture2D texture;
    const int textureResolution = 50;

    // Biome noise filter
    INoiseFilter biomeNoisefilter;

    // Colour generator
    public void UpdateSettings(ColourSettings settings) {

        //this settings = settings
        this.settings = settings;

        // If current texture is = null or the biome length is not equal to the texture height
        if (texture == null || texture.height != settings.biomeColourSettings.biomes.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            texture = new Texture2D(textureResolution * 2, settings.biomeColourSettings.biomes.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilter = NoiseFilterFactory.CreateNoiseFilter(settings.biomeColourSettings.noise);
        
    }

    // Update elevation
    public void UpdateElevation(MinMax elevationMinMax) {

        // Set planet material based on the elevation of the geometry
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));

    }

    // return value depending on the current biome
    public float BiomePercentFromPoint (Vector3 pointOnUnitSphere) {

        // Hieght percent float
        float heightPercent = (pointOnUnitSphere.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        heightPercent += (biomeNoisefilter.Evaluate(pointOnUnitSphere) - settings.biomeColourSettings.noiseOffset) * settings.biomeColourSettings.noiseStrength;

        // biome index = 0
        float biomeIndex = 0;

        // Number of biomes depending on the biome length
        int numBiomes = settings.biomeColourSettings.biomes.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float blendRange = settings.biomeColourSettings.blendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < numBiomes; i++) {

            // Float distance for the biome settings
            float dst = heightPercent - settings.biomeColourSettings.biomes[i].startHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-blendRange, blendRange, dst);

            // Reset biome index to 0
            biomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            biomeIndex += i * weight;

        }

        // return biome index
        return biomeIndex / Mathf.Max(1, numBiomes - 1);

    }

    // Update Colours
    public void UpdateColours() {

        // new colour array for texture resolution
        Color[] colours = new Color[texture.width * texture.height];

        // Colour index
        int colourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var biome in settings.biomeColourSettings.biomes) {

            // for loop for texture resolution
            for (int i = 0; i < textureResolution * 2; i++) {

                // Colour for colour gradient
                Color gradientCol;

                // If i is less than the texture resolution
                if (i < textureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    gradientCol = settings.oceanColour.Evaluate(i / (textureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    gradientCol = biome.gradient.Evaluate((i - textureResolution) / (textureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color tintCol = biome.tint;

                // gradient colour with biome tint 
                colours[colourIndex] = gradientCol * (1 - biome.tintPercent) + tintCol * biome.tintPercent;

                // Increment colour index
                colourIndex++;

            }

        }

        

        // Set colour of texture, apply texture, set planet material to texture
        texture.SetPixels(colours);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);

    }

}