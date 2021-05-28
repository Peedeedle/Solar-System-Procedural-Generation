////////////////////////////////////////////////////////////
// File:                 <ColourSettingsJupiter.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto Jupiter>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsJupiter : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsJupiter biomeColourSettingsJupiter;

    // Planet material
    public Material JupiterMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourJupiter;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsJupiter
    {

        // Public biome array
        public BiomeJupiter[] biomesJupiter;

        // noise settings
        public NoiseSettingsJupiter noiseJupiter;

        // Float for noise offset
        public float JupiternoiseOffset;

        // Float for noise strength
        public float JupiternoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float JupiterblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeJupiter
        {

            // Public Gradient
            public Gradient Jupitergradient;

            // Public tint
            public Color Jupitertint;

            // Start height of the biome range
            [Range(0,1)]
            public float JupiterstartHeight;

            // Tint percent
            [Range(0, 1)]
            public float JupitertintPercent;

        }

    }

}