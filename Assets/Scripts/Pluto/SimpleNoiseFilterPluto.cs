////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Pluto>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterPluto : INoiseFilterPluto
{

    // noise settings
    NoiseSettingsPluto.SimpleNoiseSettingsPluto settingsPluto;

    // Noise 
    NoisePluto noisePluto = new NoisePluto();

    // Contructor to set noise settings
    public SimpleNoiseFilterPluto(NoiseSettingsPluto.SimpleNoiseSettingsPluto settingsPluto) {

        // this reference
        this.settingsPluto = settingsPluto;

    }

    // Evaluate point
    public float EvaluatePluto(Vector3 point) {

        // Noise value float
        float noiseValuePluto = 0;

        // frequency float value
        float frequency = settingsPluto.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsPluto.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noisePluto.EvaluatePluto(point * frequency + settingsPluto.centre);
            noiseValuePluto += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsPluto.roughness;
            amplitude *= settingsPluto.persistence;

        }

        // Make terrain receed into planet
        noiseValuePluto = noiseValuePluto - settingsPluto.minValue;

        // Return noise value
        return noiseValuePluto * settingsPluto.strength;
    }

}