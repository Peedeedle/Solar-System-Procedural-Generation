////////////////////////////////////////////////////////////
// File:                 <ColourGeneratorPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and texture settings onto Uranus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGeneratorPluto
{

    //Colour settings reference
    ColourSettingsPluto settingsPluto;

    // 2D texture and constant int resolution for texture
    Texture2D texturePluto;
    const int PlutotextureResolution = 50;

    // Biome noise filter
    INoiseFilterPluto biomeNoisefilterPluto;

    // Colour generator
    public void UpdateSettingsPluto(ColourSettingsPluto settingsPluto) {

        //this settings = settings
        this.settingsPluto = settingsPluto;

        // If current texture is = null or the biome length is not equal to the texture height
        if (texturePluto == null || texturePluto.height != settingsPluto.biomeColourSettingsPluto.biomesPluto.Length) {

            // new texture with width of texture resolution and height of 1
            //texture = new Texture2D(textureResolution, 1);
            texturePluto = new Texture2D(PlutotextureResolution * 2, settingsPluto.biomeColourSettingsPluto.biomesPluto.Length, TextureFormat.RGBA32, false);

        }

        // Biome noise filter with noise filter factory settings 
        biomeNoisefilterPluto = NoiseFilterFactoryPluto.CreateNoiseFilterPluto(settingsPluto.biomeColourSettingsPluto.noisePluto);
        
    }

    // Update elevation
    public void UpdateElevationPluto(MinMaxPluto elevationMinMaxPluto) {

        // Set planet material based on the elevation of the geometry
        settingsPluto.PlutoMaterial.SetVector("_elevationMinMaxPluto", new Vector4(elevationMinMaxPluto.MinPluto, elevationMinMaxPluto.MaxPluto));

    }

    // return value depending on the current biome
    public float BiomePercentFromPointPluto(Vector3 pointOnUnitSpherePluto) {

        // Hieght percent float
        float PlutoheightPercent = (pointOnUnitSpherePluto.y + 1) / 2;

        // height percent and control on how far the noise moves the biomes up and down as well as how much strength is added
        PlutoheightPercent += (biomeNoisefilterPluto.EvaluatePluto(pointOnUnitSpherePluto) - settingsPluto.biomeColourSettingsPluto.PlutonoiseOffset) * settingsPluto.biomeColourSettingsPluto.PlutonoiseStrength;

        // biome index = 0
        float PlutobiomeIndex = 0;

        // Number of biomes depending on the biome length
        int PlutonumBiomes = settingsPluto.biomeColourSettingsPluto.biomesPluto.Length;

        // Blend range of the biomes (make sure value is always a liitle bit greater than 0)
        float PlutoblendRange = settingsPluto.biomeColourSettingsPluto.PlutoblendAmount / 2f + .001f;

        // for loop for number of biomes
        for (int i = 0; i < PlutonumBiomes; i++) {

            // Float distance for the biome settings
            float dst = PlutoheightPercent - settingsPluto.biomeColourSettingsPluto.biomesPluto[i].PlutostartHeight;

            // - blend range = 0 weight and blend range = 1 weight between distance of the 2 points
            float weight = Mathf.InverseLerp(-PlutoblendRange, PlutoblendRange, dst);

            // Reset biome index to 0
            PlutobiomeIndex *= (1 - weight);

            // biome index gets increased by index of current biome and multiplied by weight of it
            PlutobiomeIndex += i * weight;

        }

        // return biome index
        return PlutobiomeIndex / Mathf.Max(1, PlutonumBiomes - 1);

    }

    // Update Colours
    public void UpdateColoursPluto() {

        // new colour array for texture resolution
        Color[] Plutocolours = new Color[texturePluto.width * texturePluto.height];

        // Colour index
        int PlutocolourIndex = 0;

        // for each biome in the biome colour settings
        foreach (var Plutobiome in settingsPluto.biomeColourSettingsPluto.biomesPluto) {

            // for loop for texture resolution
            for (int i = 0; i < PlutotextureResolution * 2; i++) {

                // Colour for colour gradient
                Color PlutogradientCol;

                // If i is less than the texture resolution
                if (i < PlutotextureResolution) {

                    // evaluate texture resolution and get colour from ocean colour
                    PlutogradientCol = settingsPluto.oceanColourPluto.Evaluate(i / (PlutotextureResolution - 1f));

                // Else
                } else {

                    // Get gradient colour from biome gradient
                    PlutogradientCol = Plutobiome.Plutogradient.Evaluate((i - PlutotextureResolution) / (PlutotextureResolution - 1f));

                }

                

                // tint colour = biome.tint
                Color PlutotintCol = Plutobiome.Plutotint;

                // gradient colour with biome tint 
                Plutocolours[PlutocolourIndex] = PlutogradientCol * (1 - Plutobiome.PlutotintPercent) + PlutotintCol * Plutobiome.PlutotintPercent;

                // Increment colour index
                PlutocolourIndex++;

            }

        }



        // Set colour of texture, apply texture, set planet material to texture
        texturePluto.SetPixels(Plutocolours);
        texturePluto.Apply();
        settingsPluto.PlutoMaterial.SetTexture("_texturePluto", texturePluto);

    }

}