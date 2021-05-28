////////////////////////////////////////////////////////////
// File:                 <ColourSettingsMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto Mars>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsMars : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsMars biomeColourSettingsMars;

    // Planet material
    public Material MarsMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourMars;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsMars {

        // Public biome array
        public BiomeMars[] biomesMars;

        // noise settings
        public NoiseSettingsMars noiseMars;

        // Float for noise offset
        public float MarsnoiseOffset;

        // Float for noise strength
        public float MarsnoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float MarsblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeMars {

            // Public Gradient
            public Gradient Marsgradient;

            // Public tint
            public Color Marstint;

            // Start height of the biome range
            [Range(0,1)]
            public float MarsstartHeight;

            // Tint percent
            [Range(0, 1)]
            public float MarstintPercent;

        }

    }

}