////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Uranus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterUranus : INoiseFilterUranus {

    // noise settings
    NoiseSettingsUranus.SimpleNoiseSettingsUranus settingsUranus;

    // Noise 
    NoiseUranus noiseUranus = new NoiseUranus();

    // Contructor to set noise settings
    public SimpleNoiseFilterUranus(NoiseSettingsUranus.SimpleNoiseSettingsUranus settingsUranus) {

        // this reference
        this.settingsUranus = settingsUranus;

    }

    // Evaluate point
    public float EvaluateUranus(Vector3 point) {

        // Noise value float
        float noiseValueUranus = 0;

        // frequency float value
        float frequency = settingsUranus.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsUranus.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseUranus.EvaluateUranus(point * frequency + settingsUranus.centre);
            noiseValueUranus += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsUranus.roughness;
            amplitude *= settingsUranus.persistence;

        }

        // Make terrain receed into planet
        noiseValueUranus = noiseValueUranus - settingsUranus.minValue;

        // Return noise value
        return noiseValueUranus * settingsUranus.strength;
    }

}