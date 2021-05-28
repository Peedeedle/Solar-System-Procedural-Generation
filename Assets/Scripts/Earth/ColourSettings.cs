////////////////////////////////////////////////////////////
// File:                 <ColourSettings.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto the planet>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettings : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettings biomeColourSettings;

    // Planet material
    public Material planetMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColour;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettings {

        // Public biome array
        public Biome[] biomes;

        // noise settings
        public NoiseSettings noise;

        // Float for noise offset
        public float noiseOffset;

        // Float for noise strength
        public float noiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float blendAmount;

        //Biome class
        [System.Serializable]
        public class Biome {

            // Public Gradient
            public Gradient gradient;

            // Public tint
            public Color tint;

            // Start height of the biome range
            [Range(0,1)]
            public float startHeight;

            // Tint percent
            [Range(0, 1)]
            public float tintPercent;

        }

    }

}