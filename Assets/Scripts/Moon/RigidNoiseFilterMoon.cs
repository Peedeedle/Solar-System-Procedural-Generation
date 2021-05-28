////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on the Moon>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterMoon : INoiseFilterMoon {

    // noise settings
    NoiseSettingsMoon.RigidNoiseSettingsMoon settingsMoon;

    // Noise 
    NoiseMoon noiseMoon = new NoiseMoon();

    // Contructor to set noise settings
    public RigidNoiseFilterMoon(NoiseSettingsMoon.RigidNoiseSettingsMoon settingsMoon) {

        // this reference
        this.settingsMoon = settingsMoon;

    }

    // Evaluate point
    public float EvaluateMoon(Vector3 point) {

        // Noise value float
        float noiseValueMoon = 0;

        // frequency float value
        float Moonfrequency = settingsMoon.baseRoughness;

        // amplitude float value
        float Moonamplitude = 1;

        //
        float Moonweight = 1;

        for (int i = 0; i < settingsMoon.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseMoon.EvaluateMoon(point * Moonfrequency + settingsMoon.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Moonweight;
            Moonweight = Mathf.Clamp01(v * settingsMoon.weightMultiplier);

            // Noise value
            noiseValueMoon += v * Moonamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Moonfrequency *= settingsMoon.roughness;
            Moonamplitude *= settingsMoon.persistence;

        }

        // Make terrain receed into planet
        noiseValueMoon = noiseValueMoon - settingsMoon.minValue;

        // Return noise value
        return noiseValueMoon * settingsMoon.strength;
    }

}