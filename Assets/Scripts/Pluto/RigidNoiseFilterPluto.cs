////////////////////////////////////////////////////////////
// File:                 <RigidNoiseFilterPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the rigid noise filter variables on Pluto>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilterPluto : INoiseFilterPluto
{

    // noise settings
    NoiseSettingsPluto.RigidNoiseSettingsPluto settingsPluto;

    // Noise 
    NoisePluto noisePluto = new NoisePluto();

    // Contructor to set noise settings
    public RigidNoiseFilterPluto(NoiseSettingsPluto.RigidNoiseSettingsPluto settingsPluto) {

        // this reference
        this.settingsPluto = settingsPluto;

    }

    // Evaluate point
    public float EvaluatePluto(Vector3 point) {

        // Noise value float
        float noiseValuePluto = 0;

        // frequency float value
        float Plutofrequency = settingsPluto.baseRoughness;

        // amplitude float value
        float Plutoamplitude = 1;

        //
        float Plutoweight = 1;

        for (int i = 0; i < settingsPluto.numLayers; i++) {

            // v = absolute noise value + frequency and amplitude
            float v = 1 - Mathf.Abs(noisePluto.EvaluatePluto(point * Plutofrequency + settingsPluto.centre));

            // squaring value
            v *= v;

            // Low regions, low detail, high regions are more detailed
            v *= Plutoweight;
            Plutoweight = Mathf.Clamp01(v * settingsPluto.weightMultiplier);

            // Noise value
            noiseValuePluto += v * Plutoamplitude;

            // set frequency and amplitude values, increase rough when 1<, decrease amplitude >1 with each layer
            Plutofrequency *= settingsPluto.roughness;
            Plutoamplitude *= settingsPluto.persistence;

        }

        // Make terrain receed into planet
        noiseValuePluto = noiseValuePluto - settingsPluto.minValue;

        // Return noise value
        return noiseValuePluto * settingsPluto.strength;
    }

}