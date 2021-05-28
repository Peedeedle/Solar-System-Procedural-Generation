////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for generating the shapes based on noise and elevation settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGeneratorVenus {

    // Shape settings
    ShapeSettingsVenus settingsVenus;

    // Noise filter array
    INoiseFilterVenus[] noiseFiltersVenus;

    // public MinMax
    public MinMaxVenus elevationMinMaxVenus;

    // Shape generator
    public void UpdateSettingsVenus(ShapeSettingsVenus settingsVenus) {

        // Shape generator settings = settings
        this.settingsVenus = settingsVenus;

        // noise filter array
        noiseFiltersVenus = new INoiseFilterVenus[settingsVenus.noiseLayersVenus.Length];


        for (int i = 0; i < noiseFiltersVenus.Length; i++) {

            //
            noiseFiltersVenus[i] = NoiseFilterFactoryVenus.CreateNoiseFilterVenus(settingsVenus.noiseLayersVenus[i].noiseSettingsVenus);

        }

        // New minMax on shape generation
        elevationMinMaxVenus = new MinMaxVenus();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationVenus(Vector3 pointOnUnitSphereVenus) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationVenus = 0;

        //  if noise filters is less than 0
        if (noiseFiltersVenus.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersVenus[0].EvaluateVenus(pointOnUnitSphereVenus);

            // if noise layer 0 is enabled
            if (settingsVenus.noiseLayersVenus[0].enabled) {

                // set elevation to first layer value
                elevationVenus = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersVenus.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsVenus.noiseLayersVenus[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsVenus.noiseLayersVenus[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationVenus += noiseFiltersVenus[i].EvaluateVenus(pointOnUnitSphereVenus) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxVenus.AddValue(elevationVenus);

        // return unit sphere with settings
        return elevationVenus;

    }

    public float GetScaledElevationVenus(float unscaledElevationVenus) {

        // Float for elevation based off the unscaled elevation
        float elevationVenus = Mathf.Max(0, unscaledElevationVenus);

        // elevation = planet radius * 1 + elevation
        elevationVenus = settingsVenus.planetRadius * (1 + elevationVenus);

        //
        return elevationVenus;

    }

}