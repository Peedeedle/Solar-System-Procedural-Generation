////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Saturn>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterSaturn : INoiseFilterSaturn {

    // noise settings
    NoiseSettingsSaturn.SimpleNoiseSettingsSaturn settingsSaturn;

    // Noise 
    NoiseSaturn noiseSaturn = new NoiseSaturn();

    // Contructor to set noise settings
    public SimpleNoiseFilterSaturn(NoiseSettingsSaturn.SimpleNoiseSettingsSaturn settingsSaturn) {

        // this reference
        this.settingsSaturn = settingsSaturn;

    }

    // Evaluate point
    public float EvaluateSaturn(Vector3 point) {

        // Noise value float
        float noiseValueSaturn = 0;

        // frequency float value
        float frequency = settingsSaturn.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsSaturn.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseSaturn.EvaluateSaturn(point * frequency + settingsSaturn.centre);
            noiseValueSaturn += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsSaturn.roughness;
            amplitude *= settingsSaturn.persistence;

        }

        // Make terrain receed into planet
        noiseValueSaturn = noiseValueSaturn - settingsSaturn.minValue;

        // Return noise value
        return noiseValueSaturn * settingsSaturn.strength;
    }

}