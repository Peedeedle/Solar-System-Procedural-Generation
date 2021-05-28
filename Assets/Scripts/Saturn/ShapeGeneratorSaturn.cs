////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for generating the shapes based on noise and elevation settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGeneratorSaturn {

    // Shape settings
    ShapeSettingsSaturn settingsSaturn;

    // Noise filter array
    INoiseFilterSaturn[] noiseFiltersSaturn;

    // public MinMax
    public MinMaxSaturn elevationMinMaxSaturn;

    // Shape generator
    public void UpdateSettingsSaturn(ShapeSettingsSaturn settingsSaturn) {

        // Shape generator settings = settings
        this.settingsSaturn = settingsSaturn;

        // noise filter array
        noiseFiltersSaturn = new INoiseFilterSaturn[settingsSaturn.noiseLayersSaturn.Length];


        for (int i = 0; i < noiseFiltersSaturn.Length; i++) {

            //
            noiseFiltersSaturn[i] = NoiseFilterFactorySaturn.CreateNoiseFilterSaturn(settingsSaturn.noiseLayersSaturn[i].noiseSettingsSaturn);

        }

        // New minMax on shape generation
        elevationMinMaxSaturn = new MinMaxSaturn();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationSaturn(Vector3 pointOnUnitSphereSaturn) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationSaturn = 0;

        //  if noise filters is less than 0
        if (noiseFiltersSaturn.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersSaturn[0].EvaluateSaturn(pointOnUnitSphereSaturn);

            // if noise layer 0 is enabled
            if (settingsSaturn.noiseLayersSaturn[0].enabled) {

                // set elevation to first layer value
                elevationSaturn = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersSaturn.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsSaturn.noiseLayersSaturn[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsSaturn.noiseLayersSaturn[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationSaturn += noiseFiltersSaturn[i].EvaluateSaturn(pointOnUnitSphereSaturn) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxSaturn.AddValue(elevationSaturn);

        // return unit sphere with settings
        return elevationSaturn;

    }

    public float GetScaledElevationSaturn(float unscaledElevationSaturn) {

        // Float for elevation based off the unscaled elevation
        float elevationSaturn = Mathf.Max(0, unscaledElevationSaturn);

        // elevation = planet radius * 1 + elevation
        elevationSaturn = settingsSaturn.planetRadius * (1 + elevationSaturn);

        //
        return elevationSaturn;

    }

}