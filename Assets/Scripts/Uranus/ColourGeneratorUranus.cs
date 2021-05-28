////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto Uranus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorUranus {

    //Colour settings reference
    ColourSettingsUranus settingsUranus;

    // 2D texture and constant int resolution for texture
    Texture2D textureUranus;
    const int UranustextureResolution = 50;

    // Biome noise filter
    INoiseFilterUranus biomeNoisefilterUranus;

    // Colour generator
    public void UpdateSettingsUranus(ColourSettingsUranus settingsUranus) {

        //this settings = settings
        this.settingsUranus = settingsUranus;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureUranus == null || textureUranus.height != settingsUranus.biomeColourSettingsUranus.biomesUranus.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureUranus = new Texture2D(UranustextureResolution * 2, settingsUranus.biomeColourSettingsUranus.biomesUranus.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterUranus = NoiseFilterFactoryUranus.CreateNoiseFilterUranus(settingsUranus.biomeColourSettingsUranus.noiseUranus);
        
    }

    // Update elevation
    public void UpdateElevationUranus(MinMaxUranus elevationMinMaxUranus) {

        // Set planet material based on the elevation of the geometry
        settingsUranus.UranusMaterial.SetVector("_elevationMinMaxUranus", new Vector4(elevationMinMaxUranus.MinUranus, elevationMinMaxUranus.MaxUranus));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointUranus(Vector3 pointOnUnitSphereUranus) {

        // Hieght percent float
        float UranusheightPercent = (pointOnUnitSphereUranus.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        UranusheightPercent += (biomeNoisefilterUranus.EvaluateUranus(pointOnUnitSphereUranus) - settingsUranus.biomeColourSettingsUranus.UranusnoiseOffset) * settingsUranus.biomeColourSettingsUranus.UranusnoiseStrength;

        // biome index = 0
        float UranusbiomeIndex = 0;

        // Number of biomes depending on the biome length
        int UranusnumBiomes = settingsUranus.biomeColourSettingsUranus.biomesUranus.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float UranusblendRange = settingsUranus.biomeColourSettingsUranus.UranusblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < UranusnumBiomes; i++) {

            // Float distance for the biome settings
            float dst = UranusheightPercent - settingsUranus.biomeColourSettingsUranus.biomesUranus[i].UranusstartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-UranusblendRange, UranusblendRange, dst);

            // Reset biome index to 0
            UranusbiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            UranusbiomeIndex += i * weight;

        }

        // return biome index
        return UranusbiomeIndex / Mathf.Max(1, UranusnumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursUranus() {

        // new colour array for texture resolution
        Color[] Uranuscolours = new Color[textureUranus.width * textureUranus.height];

        // Colour index
        int UranuscolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Uranusbiome in settingsUranus.biomeColourSettingsUranus.biomesUranus) {

            // for loop for texture resolution
            for (int i = 0; i < UranustextureResolution * 2; i++) {

                // Colour for colour gradient
                Color UranusgradientCol;

                // If i is less than the texture resolution
                if (i < UranustextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    UranusgradientCol = settingsUranus.oceanColourUranus.Evaluate(i / (UranustextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    UranusgradientCol = Uranusbiome.Uranusgradient.Evaluate((i - UranustextureResolution) / (UranustextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color UranustintCol = Uranusbiome.Uranustint;

                // gradient colour with biome tint 
                Uranuscolours[UranuscolourIndex] = UranusgradientCol * (1 - Uranusbiome.UranustintPercent) + UranustintCol * Uranusbiome.UranustintPercent;

                // Increment colour index
                UranuscolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureUranus.SetPixels(Uranuscolours);
        textureUranus.Apply();
        settingsUranus.UranusMaterial.SetTexture("_textureUranus", textureUranus);

    }

}