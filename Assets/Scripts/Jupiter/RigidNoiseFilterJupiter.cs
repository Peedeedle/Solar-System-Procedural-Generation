////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterJupiter.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Jupiter>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterJupiter : INoiseFilterJupiter
{

    // noise settings
    NoiseSettingsJupiter.RigidNoiseSettingsJupiter settingsJupiter;

    // Noise 
    NoiseJupiter noiseJupiter = new NoiseJupiter();

    // Contructor to set noise settings
    public RigidNoiseFilterJupiter(NoiseSettingsJupiter.RigidNoiseSettingsJupiter settingsJupiter) {

        // this reference
        this.settingsJupiter = settingsJupiter;

    }

    // Evaluate point
    public float EvaluateJupiter(Vector3 point) {

        // Noise value float
        float noiseValueJupiter = 0;

        // frequency float value
        float Jupiterfrequency = settingsJupiter.baseRoughness;

        // amplitude float value
        float Jupiteramplitude = 1;

        //
        float Jupiterweight = 1;

        for (int i = 0; i < settingsJupiter.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noiseJupiter.EvaluateJupiter(point * Jupiterfrequency + settingsJupiter.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Jupiterweight;
            Jupiterweight = Mathf.Clamp01(v * settingsJupiter.weightMultiplier);

            // Noise value
            noiseValueJupiter += v * Jupiteramplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Jupiterfrequency *= settingsJupiter.roughness;
            Jupiteramplitude *= settingsJupiter.persistence;

        }

        // Make terrain receed into planet
        noiseValueJupiter = noiseValueJupiter - settingsJupiter.minValue;

        // Return noise value
        return noiseValueJupiter * settingsJupiter.strength;
    }

}