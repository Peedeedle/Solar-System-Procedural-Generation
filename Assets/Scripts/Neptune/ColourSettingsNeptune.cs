////////////////////////////////////////////////////////////
// File:                 <ColourSettingsNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto the Neptune>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsNeptune : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsNeptune biomeColourSettingsNeptune;

    // Planet material
    public Material NeptuneMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourNeptune;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsNeptune {

        // Public biome array
        public BiomeNeptune[] biomesNeptune;

        // noise settings
        public NoiseSettingsNeptune noiseNeptune;

        // Float for noise offset
        public float NeptunenoiseOffset;

        // Float for noise strength
        public float NeptunenoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float NeptuneblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeNeptune {

            // Public Gradient
            public Gradient Neptunegradient;

            // Public tint
            public Color Neptunetint;

            // Start height of the biome range
            [Range(0,1)]
            public float NeptunestartHeight;

            // Tint percent
            [Range(0, 1)]
            public float NeptunetintPercent;

        }

    }

}