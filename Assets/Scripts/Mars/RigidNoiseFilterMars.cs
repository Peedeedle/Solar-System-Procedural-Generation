////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Mars>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterMars : INoiseFilterMars {

    // noise settings
    NoiseSettingsMars.RigidNoiseSettingsMars settingsMars;

    // Noise 
    NoiseMars noiseMars = new NoiseMars();

    // Contructor to set noise settings
    public RigidNoiseFilterMars(NoiseSettingsMars.RigidNoiseSettingsMars settingsMars) {

        // this reference
        this.settingsMars = settingsMars;

    }

    // Evaluate point
    public float EvaluateMars(Vector3 point) {

        // Noise value float
        float noiseValueMars = 0;

        // frequency float value
        float Marsfrequency = settingsMars.baseRoughness;

        // amplitude float value
        float Marsamplitude = 1;

        //
        float Marsweight = 1;

        for (int i = 0; i < settingsMars.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseMars.EvaluateMars(point * Marsfrequency + settingsMars.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Marsweight;
            Marsweight = Mathf.Clamp01(v * settingsMars.weightMultiplier);

            // Noise value
            noiseValueMars += v * Marsamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Marsfrequency *= settingsMars.roughness;
            Marsamplitude *= settingsMars.persistence;

        }

        // Make terrain receed into planet
        noiseValueMars = noiseValueMars - settingsMars.minValue;

        // Return noise value
        return noiseValueMars * settingsMars.strength;
    }

}