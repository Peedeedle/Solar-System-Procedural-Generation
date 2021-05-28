////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorJupiter.cs>
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

public class ShapeGeneratorJupiter
{

    // Shape settings
    ShapeSettingsJupiter settingsJupiter;

    // Noise filter array
    INoiseFilterJupiter[] noiseFiltersJupiter;

    // public MinMax
    public MinMaxJupiter elevationMinMaxJupiter;

    // Shape generator
    public void UpdateSettingsJupiter(ShapeSettingsJupiter settingsJupiter) {

        // Shape generator settings = settings
        this.settingsJupiter = settingsJupiter;

        // noise filter array
        noiseFiltersJupiter = new INoiseFilterJupiter[settingsJupiter.noiseLayersJupiter.Length];


        for (int i = 0; i < noiseFiltersJupiter.Length; i++) {

            //
            noiseFiltersJupiter[i] = NoiseFilterFactoryJupiter.CreateNoiseFilterJupiter(settingsJupiter.noiseLayersJupiter[i].noiseSettingsJupiter);

        }

        // New minMax on shape generation
        elevationMinMaxJupiter = new MinMaxJupiter();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationJupiter(Vector3 pointOnUnitSphereJupiter) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationJupiter = 0;

        //  if noise filters is less than 0
        if (noiseFiltersJupiter.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersJupiter[0].EvaluateJupiter(pointOnUnitSphereJupiter);

            // if noise layer 0 is enabled
            if (settingsJupiter.noiseLayersJupiter[0].enabled) {

                // set elevation to first layer value
                elevationJupiter = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersJupiter.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsJupiter.noiseLayersJupiter[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsJupiter.noiseLayersJupiter[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationJupiter += noiseFiltersJupiter[i].EvaluateJupiter(pointOnUnitSphereJupiter) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxJupiter.AddValue(elevationJupiter);

        // return unit sphere with settings
        return elevationJupiter;

    }

    public float GetScaledElevationJupiter(float unscaledElevationJupiter) {

        // Float for elevation based off the unscaled elevation
        float elevationJupiter = Mathf.Max(0, unscaledElevationJupiter);

        // elevation = planet radius * 1 + elevation
        elevationJupiter = settingsJupiter.planetRadius * (1 + elevationJupiter);

        //
        return elevationJupiter;

    }

}