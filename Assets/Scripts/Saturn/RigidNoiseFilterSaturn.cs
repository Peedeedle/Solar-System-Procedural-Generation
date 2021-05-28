////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Saturn>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterSaturn : INoiseFilterSaturn {

    // noise settings
    NoiseSettingsSaturn.RigidNoiseSettingsSaturn settingsSaturn;

    // Noise 
    NoiseSaturn noiseSaturn = new NoiseSaturn();

    // Contructor to set noise settings
    public RigidNoiseFilterSaturn(NoiseSettingsSaturn.RigidNoiseSettingsSaturn settingsSaturn) {

        // this reference
        this.settingsSaturn = settingsSaturn;

    }

    // Evaluate point
    public float EvaluateSaturn(Vector3 point) {

        // Noise value float
        float noiseValueSaturn = 0;

        // frequency float value
        float Saturnfrequency = settingsSaturn.baseRoughness;

        // amplitude float value
        float Saturnamplitude = 1;

        //
        float Saturnweight = 1;

        for (int i = 0; i < settingsSaturn.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseSaturn.EvaluateSaturn(point * Saturnfrequency + settingsSaturn.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Saturnweight;
            Saturnweight = Mathf.Clamp01(v * settingsSaturn.weightMultiplier);

            // Noise value
            noiseValueSaturn += v * Saturnamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Saturnfrequency *= settingsSaturn.roughness;
            Saturnamplitude *= settingsSaturn.persistence;

        }

        // Make terrain receed into planet
        noiseValueSaturn = noiseValueSaturn - settingsSaturn.minValue;

        // Return noise value
        return noiseValueSaturn * settingsSaturn.strength;
    }

}