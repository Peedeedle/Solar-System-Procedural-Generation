////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Mars>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterMars : INoiseFilterMars {

    // noise settings
    NoiseSettingsMars.SimpleNoiseSettingsMars settingsMars;

    // Noise 
    NoiseMars noiseMars = new NoiseMars();

    // Contructor to set noise settings
    public SimpleNoiseFilterMars(NoiseSettingsMars.SimpleNoiseSettingsMars settingsMars) {

        // this reference
        this.settingsMars = settingsMars;

    }

    // Evaluate point
    public float EvaluateMars(Vector3 point) {

        // Noise value float
        float noiseValueMars = 0;

        // frequency float value
        float frequency = settingsMars.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsMars.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseMars.EvaluateMars(point * frequency + settingsMars.centre);
            noiseValueMars += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsMars.roughness;
            amplitude *= settingsMars.persistence;

        }

        // Make terrain receed into planet
        noiseValueMars = noiseValueMars - settingsMars.minValue;

        // Return noise value
        return noiseValueMars * settingsMars.strength;
    }

}