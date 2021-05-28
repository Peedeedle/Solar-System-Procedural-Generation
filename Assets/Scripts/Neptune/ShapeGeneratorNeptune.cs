////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for generating the shapes based on noise and elevation settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGeneratorNeptune {

    // Shape settings
    ShapeSettingsNeptune settingsNeptune;

    // Noise filter array
    INoiseFilterNeptune[] noiseFiltersNeptune;

    // public MinMax
    public MinMaxNeptune elevationMinMaxNeptune;

    // Shape generator
    public void UpdateSettingsNeptune(ShapeSettingsNeptune settingsNeptune) {

        // Shape generator settings = settings
        this.settingsNeptune = settingsNeptune;

        // noise filter array
        noiseFiltersNeptune = new INoiseFilterNeptune[settingsNeptune.noiseLayersNeptune.Length];

        
        for (int i = 0; i < noiseFiltersNeptune.Length; i++) {

            //
            noiseFiltersNeptune[i] = NoiseFilterFactoryNeptune.CreateNoiseFilterNeptune(settingsNeptune.noiseLayersNeptune[i].noiseSettingsNeptune);

        }

        // New minMax on shape generation
        elevationMinMaxNeptune = new MinMaxNeptune();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationNeptune(Vector3 pointOnUnitSphereNeptune) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationNeptune = 0;

        //  if noise filters is less than 0
        if (noiseFiltersNeptune.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersNeptune[0].EvaluateNeptune(pointOnUnitSphereNeptune);

            // if noise layer 0 is enabled
            if (settingsNeptune.noiseLayersNeptune[0].enabled) {

                // set elevation to first layer value
                elevationNeptune = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersNeptune.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsNeptune.noiseLayersNeptune[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsNeptune.noiseLayersNeptune[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationNeptune += noiseFiltersNeptune[i].EvaluateNeptune(pointOnUnitSphereNeptune) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxNeptune.AddValue(elevationNeptune);

        // return unit sphere with settings
        return elevationNeptune;

    }

    public float GetScaledElevationNeptune(float unscaledElevationNeptune) {

        // Float for elevation based off the unscaled elevation
        float elevationNeptune = Mathf.Max(0, unscaledElevationNeptune);

        // elevation = planet radius * 1 + elevation
        elevationNeptune = settingsNeptune.planetRadius * (1 + elevationNeptune);

        //
        return elevationNeptune;

    }

}