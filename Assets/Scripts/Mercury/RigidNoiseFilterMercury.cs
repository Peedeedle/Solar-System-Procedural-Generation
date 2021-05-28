////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterMercury.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Mercury>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterMercury : INoiseFilterMercury {

    // noise settings
    NoiseSettingsMercury.RigidNoiseSettingsMercury settingsMercury;

    // Noise 
    NoiseMercury noiseMercury = new NoiseMercury();

    // Contructor to set noise settings
    public RigidNoiseFilterMercury(NoiseSettingsMercury.RigidNoiseSettingsMercury settingsMercury) {

        // this reference
        this.settingsMercury = settingsMercury;

    }

    // Evaluate point
    public float EvaluateMercury(Vector3 point) {

        // Noise value float
        float noiseValueMercury = 0;

        // frequency float value
        float Mercuryfrequency = settingsMercury.baseRoughness;

        // amplitude float value
        float Mercuryamplitude = 1;

        //
        float Mercuryweight = 1;

        for (int i = 0; i < settingsMercury.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseMercury.EvaluateMercury(point * Mercuryfrequency + settingsMercury.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Mercuryweight;
            Mercuryweight = Mathf.Clamp01(v * settingsMercury.weightMultiplier);

            // Noise value
            noiseValueMercury += v * Mercuryamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Mercuryfrequency *= settingsMercury.roughness;
            Mercuryamplitude *= settingsMercury.persistence;

        }

        // Make terrain receed into planet
        noiseValueMercury = noiseValueMercury - settingsMercury.minValue;

        // Return noise value
        return noiseValueMercury * settingsMercury.strength;
    }

}