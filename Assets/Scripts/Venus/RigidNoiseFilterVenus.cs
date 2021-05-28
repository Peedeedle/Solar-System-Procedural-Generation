////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Venus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterVenus : INoiseFilterVenus {

    // noise settings
    NoiseSettingsVenus.RigidNoiseSettingsVenus settingsVenus;

    // Noise 
    NoiseVenus noiseVenus = new NoiseVenus();

    // Contructor to set noise settings
    public RigidNoiseFilterVenus(NoiseSettingsVenus.RigidNoiseSettingsVenus settingsVenus) {

        // this reference
        this.settingsVenus = settingsVenus;

    }

    // Evaluate point
    public float EvaluateVenus(Vector3 point) {

        // Noise value float
        float noiseValueVenus = 0;

        // frequency float value
        float Venusfrequency = settingsVenus.baseRoughness;

        // amplitude float value
        float Venusamplitude = 1;

        //
        float Venusweight = 1;

        for (int i = 0; i < settingsVenus.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseVenus.EvaluateVenus(point * Venusfrequency + settingsVenus.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Venusweight;
            Venusweight = Mathf.Clamp01(v * settingsVenus.weightMultiplier);

            // Noise value
            noiseValueVenus += v * Venusamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Venusfrequency *= settingsVenus.roughness;
            Venusamplitude *= settingsVenus.persistence;

        }

        // Make terrain receed into planet
        noiseValueVenus = noiseValueVenus - settingsVenus.minValue;

        // Return noise value
        return noiseValueVenus * settingsVenus.strength;
    }

}