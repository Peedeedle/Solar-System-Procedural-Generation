////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Venus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterVenus : INoiseFilterVenus {

    // noise settings
    NoiseSettingsVenus.SimpleNoiseSettingsVenus settingsVenus;

    // Noise 
    NoiseVenus noiseVenus = new NoiseVenus();

    // Contructor to set noise settings
    public SimpleNoiseFilterVenus(NoiseSettingsVenus.SimpleNoiseSettingsVenus settingsVenus) {

        // this reference
        this.settingsVenus = settingsVenus;

    }

    // Evaluate point
    public float EvaluateVenus(Vector3 point) {

        // Noise value float
        float noiseValueVenus = 0;

        // frequency float value
        float frequency = settingsVenus.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsVenus.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseVenus.EvaluateVenus(point * frequency + settingsVenus.centre);
            noiseValueVenus += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsVenus.roughness;
            amplitude *= settingsVenus.persistence;

        }

        // Make terrain receed into planet
        noiseValueVenus = noiseValueVenus - settingsVenus.minValue;

        // Return noise value
        return noiseValueVenus * settingsVenus.strength;
    }

}