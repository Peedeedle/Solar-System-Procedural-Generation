////////////////////////////////////////////////////////////
// File:                 <ColourSettingsUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto Uranus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsUranus : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsUranus biomeColourSettingsUranus;

    // Planet material
    public Material UranusMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourUranus;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsUranus {

        // Public biome array
        public BiomeUranus[] biomesUranus;

        // noise settings
        public NoiseSettingsUranus noiseUranus;

        // Float for noise offset
        public float UranusnoiseOffset;

        // Float for noise strength
        public float UranusnoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float UranusblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeUranus {

            // Public Gradient
            public Gradient Uranusgradient;

            // Public tint
            public Color Uranustint;

            // Start height of the biome range
            [Range(0,1)]
            public float UranusstartHeight;

            // Tint percent
            [Range(0, 1)]
            public float UranustintPercent;

        }

    }

}