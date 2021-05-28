////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Neptune>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterNeptune : INoiseFilterNeptune {

    // noise settings
    NoiseSettingsNeptune.RigidNoiseSettingsNeptune settingsNeptune;

    // Noise 
    NoiseNeptune noiseNeptune = new NoiseNeptune();

    // Contructor to set noise settings
    public RigidNoiseFilterNeptune(NoiseSettingsNeptune.RigidNoiseSettingsNeptune settingsNeptune) {

        // this reference
        this.settingsNeptune = settingsNeptune;

    }

    // Evaluate point
    public float EvaluateNeptune(Vector3 point) {

        // Noise value float
        float noiseValueNeptune = 0;

        // frequency float value
        float Neptunefrequency = settingsNeptune.baseRoughness;

        // amplitude float value
        float Neptuneamplitude = 1;

        //
        float Neptuneweight = 1;

        for (int i = 0; i < settingsNeptune.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseNeptune.EvaluateNeptune(point * Neptunefrequency + settingsNeptune.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Neptuneweight;
            Neptuneweight = Mathf.Clamp01(v * settingsNeptune.weightMultiplier);

            // Noise value
            noiseValueNeptune += v * Neptuneamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Neptunefrequency *= settingsNeptune.roughness;
            Neptuneamplitude *= settingsNeptune.persistence;

        }

        // Make terrain receed into planet
        noiseValueNeptune = noiseValueNeptune - settingsNeptune.minValue;

        // Return noise value
        return noiseValueNeptune * settingsNeptune.strength;
    }

}