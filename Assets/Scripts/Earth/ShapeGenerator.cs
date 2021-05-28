////////////////////////////////////////////////////////////
// File:                 <ShapeGenerator.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for generating the shapes based on noise and elevation settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator {

    // Shape settings
    ShapeSettings settings;

    // Noise filter array
    INoiseFilter[] noiseFilters;

    // public MinMax
    public MinMax elevationMinMax;

    // Shape generator
    public void UpdateSettings(ShapeSettings settings) {

        // Shape generator settings = settings
        this.settings = settings;

        // noise filter array
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];

        
        for (int i = 0; i < noiseFilters.Length; i++) {

            //
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);

        }

        // New minMax on shape generation
        elevationMinMax = new MinMax();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevation (Vector3 pointOnUnitSphere) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevation = 0;

        //  if noise filters is less than 0
        if (noiseFilters.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);

            // if noise layer 0 is enabled
            if (settings.noiseLayers[0].enabled) {

                // set elevation to first layer value
                elevation = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFilters.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settings.noiseLayers[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMax.AddValue(elevation);

        // return unit sphere with settings
        return elevation;

    }

    public float GetScaledElevation (float unscaledElevation) {

        // Float for elevation based off the unscaled elevation
        float elevation = Mathf.Max(0, unscaledElevation);

        // elevation = planet radius * 1 + elevation
        elevation = settings.planetRadius * (1 + elevation);

        //
        return elevation;

    }

}