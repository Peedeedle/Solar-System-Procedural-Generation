////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterJupiter.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Jupiter>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterJupiter : INoiseFilterJupiter
{

    // noise settings
    NoiseSettingsJupiter.SimpleNoiseSettingsJupiter settingsJupiter;

    // Noise 
    NoiseJupiter noiseJupiter = new NoiseJupiter();

    // Contructor to set noise settings
    public SimpleNoiseFilterJupiter(NoiseSettingsJupiter.SimpleNoiseSettingsJupiter settingsJupiter) {

        // this reference
        this.settingsJupiter = settingsJupiter;

    }

    // Evaluate point
    public float EvaluateJupiter(Vector3 point) {

        // Noise value float
        float noiseValueJupiter = 0;

        // frequency float value
        float frequency = settingsJupiter.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsJupiter.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseJupiter.EvaluateJupiter(point * frequency + settingsJupiter.centre);
            noiseValueJupiter += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsJupiter.roughness;
            amplitude *= settingsJupiter.persistence;

        }

        // Make terrain receed into planet
        noiseValueJupiter = noiseValueJupiter - settingsJupiter.minValue;

        // Return noise value
        return noiseValueJupiter * settingsJupiter.strength;
    }

}