////////////////////////////////////////////////////////////
// File:                 <ShapeGeneratorMercury.cs>
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

public class ShapeGeneratorMercury {

    // Shape settings
    ShapeSettingsMercury settingsMercury;

    // Noise filter array
    INoiseFilterMercury[] noiseFiltersMercury;

    // public MinMax
    public MinMaxMercury elevationMinMaxMercury;

    // Shape generator
    public void UpdateSettingsMercury(ShapeSettingsMercury settingsMercury) {

        // Shape generator settings = settings
        this.settingsMercury = settingsMercury;

        // noise filter array
        noiseFiltersMercury = new INoiseFilterMercury[settingsMercury.noiseLayersMercury.Length];


        for (int i = 0; i < noiseFiltersMercury.Length; i++) {

            //
            noiseFiltersMercury[i] = NoiseFilterFactoryMercury.CreateNoiseFilterMercury(settingsMercury.noiseLayersMercury[i].noiseSettingsMercury);

        }

        // New minMax on shape generation
        elevationMinMaxMercury = new MinMaxMercury();

    }

    // Calculate point on planet
    public float CalculateUnscaledElevationMercury(Vector3 pointOnUnitSphereMercury) {

        // first layer value
        float firstLayerValue = 0;

        // evelation float
        float elevationMercury = 0;

        //  if noise filters is less than 0
        if (noiseFiltersMercury.Length > 0) {

            // first layer value = 0
            firstLayerValue = noiseFiltersMercury[0].EvaluateMercury(pointOnUnitSphereMercury);

            // if noise layer 0 is enabled
            if (settingsMercury.noiseLayersMercury[0].enabled) {

                // set elevation to first layer value
                elevationMercury = firstLayerValue;

            }

        }

        //loop through noise settings
        for (int i = 0; i < noiseFiltersMercury.Length; i++) {

            // Only add elevation and noise if enabled is true
            if (settingsMercury.noiseLayersMercury[i].enabled) {

                // float mask, depends on if the noise layer is using first layer as mask, if it is the mask is = first layer value, otherwise it is = 1 (no mask)
                float mask = (settingsMercury.noiseLayersMercury[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // elevation of noise
                elevationMercury += noiseFiltersMercury[i].EvaluateMercury(pointOnUnitSphereMercury) * mask;

            }
            
        }

        // keep track of minimum and maximum elevation for planet
        elevationMinMaxMercury.AddValue(elevationMercury);

        // return unit sphere with settings
        return elevationMercury;

    }

    public float GetScaledElevationMercury(float unscaledElevationMercury) {

        // Float for elevation based off the unscaled elevation
        float elevationMercury = Mathf.Max(0, unscaledElevationMercury);

        // elevation = planet radius * 1 + elevation
        elevationMercury = settingsMercury.planetRadius * (1 + elevationMercury);

        //
        return elevationMercury;

    }

}