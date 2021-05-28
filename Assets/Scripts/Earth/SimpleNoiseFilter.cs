////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilter.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on the planet>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter {

    // noise settings
    NoiseSettings.SimpleNoiseSettings settings;

    // Noise 
    Noise noise = new Noise();

    // Contructor to set noise settings
    public SimpleNoiseFilter (NoiseSettings.SimpleNoiseSettings settings) {

        // this reference
        this.settings = settings;

    }

    // Evaluate point
    public float Evaluate(Vector3 point) {

        // Noise value float
        float noiseValue = 0;

        // frequency float value
        float frequency = settings.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settings.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settings.roughness;
            amplitude *= settings.persistence;

        }

        // Make terrain receed into planet
        noiseValue = noiseValue - settings.minValue;

        // Return noise value
        return noiseValue * settings.strength;
    }

}