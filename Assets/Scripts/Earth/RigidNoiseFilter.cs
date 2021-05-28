////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilter.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on the planet>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter {

    // noise settings
    NoiseSettings.RigidNoiseSettings settings;

    // Noise 
    Noise noise = new Noise();

    // Contructor to set noise settings
    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings) {

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

        //
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);

            // Noise value
            noiseValue += v * amplitude;

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