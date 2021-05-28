////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on the Moon>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterMoon : INoiseFilterMoon {

    // noise settings
    NoiseSettingsMoon.SimpleNoiseSettingsMoon settingsMoon;

    // Noise 
    NoiseMoon noiseMoon = new NoiseMoon();

    // Contructor to set noise settings
    public SimpleNoiseFilterMoon(NoiseSettingsMoon.SimpleNoiseSettingsMoon settingsMoon) {

        // this reference
        this.settingsMoon = settingsMoon;

    }

    // Evaluate point
    public float EvaluateMoon(Vector3 point) {

        // Noise value float
        float noiseValueMoon = 0;

        // frequency float value
        float frequency = settingsMoon.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsMoon.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseMoon.EvaluateMoon(point * frequency + settingsMoon.centre);
            noiseValueMoon += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsMoon.roughness;
            amplitude *= settingsMoon.persistence;

        }

        // Make terrain receed into planet
        noiseValueMoon = noiseValueMoon - settingsMoon.minValue;

        // Return noise value
        return noiseValueMoon * settingsMoon.strength;
    }

}