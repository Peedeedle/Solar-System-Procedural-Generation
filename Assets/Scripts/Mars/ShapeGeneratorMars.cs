////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorMars.cs>
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

public class ShapeGeneratorMars {

    // Shape settings
    ShapeSettingsMars settingsMars;

    // Noise filter array
    INoiseFilterMars[] noiseFiltersMars;

    // public MinMax
    public MinMaxMars elevationMinMaxMars;

    // Shape generator
    public void UpdateSettingsMars(ShapeSettingsMars settingsMars) {

        // Shape generator settings = settings
        this.settingsMars = settingsMars;

        // noise filter array
        noiseFiltersMars = new INoiseFilterMars[settingsMars.noiseLayersMars.Length];


        for (int i = 0; i < noiseFiltersMars.Length; i++) {

            //
            noiseFiltersMars[i] = NoiseFilterFactoryMars.CreateNoiseFilterMars(settingsMars.noiseLayersMars[i].noiseSettingsMars);

        }

        // New minMax on shape generation
        elevationMinMaxMars = new MinMaxMars();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationMars(Vector3 pointOnUnitSphereMars) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationMars = 0;

        //  if noise filters is less than 0
        if (noiseFiltersMars.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersMars[0].EvaluateMars(pointOnUnitSphereMars);

            // if noise layer 0 is enabled
            if (settingsMars.noiseLayersMars[0].enabled) {

                // set elevation to first layer value
                elevationMars = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersMars.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsMars.noiseLayersMars[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsMars.noiseLayersMars[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationMars += noiseFiltersMars[i].EvaluateMars(pointOnUnitSphereMars) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxMars.AddValue(elevationMars);

        // return unit sphere with settings
        return elevationMars;

    }

    public float GetScaledElevationMars(float unscaledElevationMars) {

        // Float for elevation based off the unscaled elevation
        float elevationMars = Mathf.Max(0, unscaledElevationMars);

        // elevation = planet radius * 1 + elevation
        elevationMars = settingsMars.planetRadius * (1 + elevationMars);

        //
        return elevationMars;

    }

}