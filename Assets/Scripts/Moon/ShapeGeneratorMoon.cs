////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for generating the shapes based on noise and elevation settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGeneratorMoon {

    // Shape settings
    ShapeSettingsMoon settingsMoon;

    // Noise filter array
    INoiseFilterMoon[] noiseFiltersMoon;

    // public MinMax
    public MinMaxMoon elevationMinMaxMoon;

    // Shape generator
    public void UpdateSettingsMoon(ShapeSettingsMoon settingsMoon) {

        // Shape generator settings = settings
        this.settingsMoon = settingsMoon;

        // noise filter array
        noiseFiltersMoon = new INoiseFilterMoon[settingsMoon.noiseLayersMoon.Length];

        
        for (int i = 0; i < noiseFiltersMoon.Length; i++) {

            //
            noiseFiltersMoon[i] = NoiseFilterFactoryMoon.CreateNoiseFilterMoon(settingsMoon.noiseLayersMoon[i].noiseSettingsMoon);

        }

        // New minMax on shape generation
        elevationMinMaxMoon = new MinMaxMoon();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationMoon(Vector3 pointOnUnitSphereMoon) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationMoon = 0;

        //  if noise filters is less than 0
        if (noiseFiltersMoon.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersMoon[0].EvaluateMoon(pointOnUnitSphereMoon);

            // if noise layer 0 is enabled
            if (settingsMoon.noiseLayersMoon[0].enabled) {

                // set elevation to first layer value
                elevationMoon = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersMoon.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsMoon.noiseLayersMoon[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsMoon.noiseLayersMoon[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationMoon += noiseFiltersMoon[i].EvaluateMoon(pointOnUnitSphereMoon) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxMoon.AddValue(elevationMoon);

        // return unit sphere with settings
        return elevationMoon;

    }

    public float GetScaledElevationMoon(float unscaledElevationMoon) {

        // Float for elevation based off the unscaled elevation
        float elevationMoon = Mathf.Max(0, unscaledElevationMoon);

        // elevation = planet radius * 1 + elevation
        elevationMoon = settingsMoon.planetRadius * (1 + elevationMoon);

        //
        return elevationMoon;

    }

}