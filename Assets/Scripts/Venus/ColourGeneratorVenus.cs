////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto Venus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorVenus {

    //Colour settings reference
    ColourSettingsVenus settingsVenus;

    // 2D texture and constant int resolution for texture
    Texture2D textureVenus;
    const int VenustextureResolution = 50;

    // Biome noise filter
    INoiseFilterVenus biomeNoisefilterVenus;

    // Colour generator
    public void UpdateSettingsVenus(ColourSettingsVenus settingsVenus) {

        //this settings = settings
        this.settingsVenus = settingsVenus;

        // If current texture is = null or the biome length is not equal to the texture height
        if (textureVenus == null || textureVenus.height != settingsVenus.biomeColourSettingsVenus.biomesVenus.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            textureVenus = new Texture2D(VenustextureResolution * 2, settingsVenus.biomeColourSettingsVenus.biomesVenus.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterVenus = NoiseFilterFactoryVenus.CreateNoiseFilterVenus(settingsVenus.biomeColourSettingsVenus.noiseVenus);
        
    }

    // Update elevation
    public void UpdateElevationVenus(MinMaxVenus elevationMinMaxVenus) {

        // Set planet material based on the elevation of the geometry
        settingsVenus.VenusMaterial.SetVector("_elevationMinMaxVenus", new Vector4(elevationMinMaxVenus.MinVenus, elevationMinMaxVenus.MaxVenus));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointVenus(Vector3 pointOnUnitSphereVenus) {

        // Hieght percent float
        float VenusheightPercent = (pointOnUnitSphereVenus.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        VenusheightPercent += (biomeNoisefilterVenus.EvaluateVenus(pointOnUnitSphereVenus) - settingsVenus.biomeColourSettingsVenus.VenusnoiseOffset) * settingsVenus.biomeColourSettingsVenus.VenusnoiseStrength;

        // biome index = 0
        float VenusbiomeIndex = 0;

        // Number of biomes depending on the biome length
        int VenusnumBiomes = settingsVenus.biomeColourSettingsVenus.biomesVenus.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float VenusblendRange = settingsVenus.biomeColourSettingsVenus.VenusblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < VenusnumBiomes; i++) {

            // Float distance for the biome settings
            float dst = VenusheightPercent - settingsVenus.biomeColourSettingsVenus.biomesVenus[i].VenusstartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-VenusblendRange, VenusblendRange, dst);

            // Reset biome index to 0
            VenusbiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            VenusbiomeIndex += i * weight;

        }

        // return biome index
        return VenusbiomeIndex / Mathf.Max(1, VenusnumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursVenus() {

        // new colour array for texture resolution
        Color[] Venuscolours = new Color[textureVenus.width * textureVenus.height];

        // Colour index
        int VenuscolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Venusbiome in settingsVenus.biomeColourSettingsVenus.biomesVenus) {

            // for loop for texture resolution
            for (int i = 0; i < VenustextureResolution * 2; i++) {

                // Colour for colour gradient
                Color VenusgradientCol;

                // If i is less than the texture resolution
                if (i < VenustextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    VenusgradientCol = settingsVenus.oceanColourVenus.Evaluate(i / (VenustextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    VenusgradientCol = Venusbiome.Venusgradient.Evaluate((i - VenustextureResolution) / (VenustextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color VenustintCol = Venusbiome.Venustint;

                // gradient colour with biome tint 
                Venuscolours[VenuscolourIndex] = VenusgradientCol * (1 - Venusbiome.VenustintPercent) + VenustintCol * Venusbiome.VenustintPercent;

                // Increment colour index
                VenuscolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        textureVenus.SetPixels(Venuscolours);
        textureVenus.Apply();
        settingsVenus.VenusMaterial.SetTexture("_textureVenus", textureVenus);

    }

}