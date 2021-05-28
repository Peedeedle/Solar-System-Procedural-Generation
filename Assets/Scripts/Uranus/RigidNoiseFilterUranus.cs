////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Uranus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterUranus : INoiseFilterUranus {

    // noise settings
    NoiseSettingsUranus.RigidNoiseSettingsUranus settingsUranus;

    // Noise 
    NoiseUranus noiseUranus = new NoiseUranus();

    // Contructor to set noise settings
    public RigidNoiseFilterUranus(NoiseSettingsUranus.RigidNoiseSettingsUranus settingsUranus) {

        // this reference
        this.settingsUranus = settingsUranus;

    }

    // Evaluate point
    public float EvaluateUranus(Vector3 point) {

        // Noise value float
        float noiseValueUranus = 0;

        // frequency float value
        float Uranusfrequency = settingsUranus.baseRoughness;

        // amplitude float value
        float Uranusamplitude = 1;

        //
        float Uranusweight = 1;

        for (int i = 0; i < settingsUranus.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseUranus.EvaluateUranus(point * Uranusfrequency + settingsUranus.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Uranusweight;
            Uranusweight = Mathf.Clamp01(v * settingsUranus.weightMultiplier);

            // Noise value
            noiseValueUranus += v * Uranusamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Uranusfrequency *= settingsUranus.roughness;
            Uranusamplitude *= settingsUranus.persistence;

        }

        // Make terrain receed into planet
        noiseValueUranus = noiseValueUranus - settingsUranus.minValue;

        // Return noise value
        return noiseValueUranus * settingsUranus.strength;
    }

}