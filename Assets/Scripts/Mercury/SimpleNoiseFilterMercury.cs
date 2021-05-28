////////////////////////////////////////////////////////////
// File:                 <SimpleNoiseFilterMercury.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the simple noise filter variables on Mercury>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilterMercury : INoiseFilterMercury {

    // noise settings
    NoiseSettingsMercury.SimpleNoiseSettingsMercury settingsMercury;

    // Noise 
    NoiseMercury noiseMercury = new NoiseMercury();

    // Contructor to set noise settings
    public SimpleNoiseFilterMercury(NoiseSettingsMercury.SimpleNoiseSettingsMercury settingsMercury) {

        // this reference
        this.settingsMercury = settingsMercury;

    }

    // Evaluate point
    public float EvaluateMercury(Vector3 point) {

        // Noise value float
        float noiseValueMercury = 0;

        // frequency float value
        float frequency = settingsMercury.baseRoughness;

        // amplitude float value
        float amplitude = 1;

        for (int i = 0; i < settingsMercury.numLayers; i++) {

            // v = noise value + frequency and amplitude
            float v = noiseMercury.EvaluateMercury(point * frequency + settingsMercury.centre);
            noiseValueMercury += (v + 1) * 0.5f * amplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            frequency *= settingsMercury.roughness;
            amplitude *= settingsMercury.persistence;

        }

        // Make terrain receed into planet
        noiseValueMercury = noiseValueMercury - settingsMercury.minValue;

        // Return noise value
        return noiseValueMercury * settingsMercury.strength;
    }

}