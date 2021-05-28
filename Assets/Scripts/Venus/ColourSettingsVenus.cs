////////////////////////////////////////////////////////////
// File:                 <ColourSettingsVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto Venus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsVenus : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsVenus biomeColourSettingsVenus;

    // Planet material
    public Material VenusMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourVenus;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsVenus {

        // Public biome array
        public BiomeVenus[] biomesVenus;

        // noise settings
        public NoiseSettingsVenus noiseVenus;

        // Float for noise offset
        public float VenusnoiseOffset;

        // Float for noise strength
        public float VenusnoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float VenusblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeVenus {

            // Public Gradient
            public Gradient Venusgradient;

            // Public tint
            public Color Venustint;

            // Start height of the biome range
            [Range(0,1)]
            public float VenusstartHeight;

            // Tint percent
            [Range(0, 1)]
            public float VenustintPercent;

        }

    }

}