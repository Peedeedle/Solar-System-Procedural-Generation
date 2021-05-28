////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Neptune>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterNeptune : INoiseFilterNeptune {

    // noise settings
    NoiseSettingsNeptune.SimpleNoiseSettingsNeptune settingsNeptune;

    // Noise 
    NoiseNeptune noiseNeptune = new NoiseNeptune();

    // Contructor to set noise settings
    public SimpleNoiseFilterNeptune(NoiseSettingsNeptune.SimpleNoiseSettingsNeptune settingsNeptune) {

        // this reference
        this.settingsNeptune = settingsNeptune;

    }

    // Evaluate point
    public float EvaluateNeptune(Vector3 point) {

        // Noise value float
        float noiseValueNeptune = 0;

        // frequency float value
        float frequency = settingsNeptune.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsNeptune.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseNeptune.EvaluateNeptune(point * frequency + settingsNeptune.centre);
            noiseValueNeptune += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsNeptune.roughness;
            amplitude *= settingsNeptune.persistence;

        }

        // Make terrain receed into planet
        noiseValueNeptune = noiseValueNeptune - settingsNeptune.minValue;

        // Return noise value
        return noiseValueNeptune * settingsNeptune.strength;
    }

}