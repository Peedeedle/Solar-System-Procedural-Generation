////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for generating the shapes based on noise and elevation settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGeneratorUranus {

    // Shape settings
    ShapeSettingsUranus settingsUranus;

    // Noise filter array
    INoiseFilterUranus[] noiseFiltersUranus;

    // public MinMax
    public MinMaxUranus elevationMinMaxUranus;

    // Shape generator
    public void UpdateSettingsUranus(ShapeSettingsUranus settingsUranus) {

        // Shape generator settings = settings
        this.settingsUranus = settingsUranus;

        // noise filter array
        noiseFiltersUranus = new INoiseFilterUranus[settingsUranus.noiseLayersUranus.Length];

        
        for (int i = 0; i < noiseFiltersUranus.Length; i++) {

            //
            noiseFiltersUranus[i] = NoiseFilterFactoryUranus.CreateNoiseFilterUranus(settingsUranus.noiseLayersUranus[i].noiseSettingsUranus);

        }

        // New minMax on shape generation
        elevationMinMaxUranus = new MinMaxUranus();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationUranus(Vector3 pointOnUnitSphereUranus) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationUranus = 0;

        //  if noise filters is less than 0
        if (noiseFiltersUranus.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersUranus[0].EvaluateUranus(pointOnUnitSphereUranus);

            // if noise layer 0 is enabled
            if (settingsUranus.noiseLayersUranus[0].enabled) {

                // set elevation to first layer value
                elevationUranus = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersUranus.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsUranus.noiseLayersUranus[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsUranus.noiseLayersUranus[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationUranus += noiseFiltersUranus[i].EvaluateUranus(pointOnUnitSphereUranus) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxUranus.AddValue(elevationUranus);

        // return unit sphere with settings
        return elevationUranus;

    }

    public float GetScaledElevationUranus(float unscaledElevationUranus) {

        // Float for elevation based off the unscaled elevation
        float elevationUranus = Mathf.Max(0, unscaledElevationUranus);

        // elevation = planet radius * 1 + elevation
        elevationUranus = settingsUranus.planetRadius * (1 + elevationUranus);

        //
        return elevationUranus;

    }

}