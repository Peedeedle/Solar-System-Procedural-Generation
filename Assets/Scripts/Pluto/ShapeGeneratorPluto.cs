////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for generating the shapes based on noise and elevation settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGeneratorPluto
{

    // Shape settings
    ShapeSettingsPluto settingsPluto;

    // Noise filter array
    INoiseFilterPluto[] noiseFiltersPluto;

    // public MinMax
    public MinMaxPluto elevationMinMaxPluto;

    // Shape generator
    public void UpdateSettingsPluto(ShapeSettingsPluto settingsPluto) {

        // Shape generator settings = settings
        this.settingsPluto = settingsPluto;

        // noise filter array
        noiseFiltersPluto = new INoiseFilterPluto[settingsPluto.noiseLayersPluto.Length];


        for (int i = 0; i < noiseFiltersPluto.Length; i++) {

            //
            noiseFiltersPluto[i] = NoiseFilterFactoryPluto.CreateNoiseFilterPluto(settingsPluto.noiseLayersPluto[i].noiseSettingsPluto);

        }

        // New minMax on shape generation
        elevationMinMaxPluto = new MinMaxPluto();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationPluto(Vector3 pointOnUnitSpherePluto) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationPluto = 0;

        //  if noise filters is less than 0
        if (noiseFiltersPluto.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersPluto[0].EvaluatePluto(pointOnUnitSpherePluto);

            // if noise layer 0 is enabled
            if (settingsPluto.noiseLayersPluto[0].enabled) {

                // set elevation to first layer value
                elevationPluto = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersPluto.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsPluto.noiseLayersPluto[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsPluto.noiseLayersPluto[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationPluto += noiseFiltersPluto[i].EvaluatePluto(pointOnUnitSpherePluto) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxPluto.AddValue(elevationPluto);

        // return unit sphere with settings
        return elevationPluto;

    }

    public float GetScaledElevationPluto(float unscaledElevationPluto) {

        // Float for elevation based off the unscaled elevation
        float elevationPluto = Mathf.Max(0, unscaledElevationPluto);

        // elevation = planet radius * 1 + elevation
        elevationPluto = settingsPluto.planetRadius * (1 + elevationPluto);

        //
        return elevationPluto;

    }

}