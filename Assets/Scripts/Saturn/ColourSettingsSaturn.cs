////////////////////////////////////////////////////////////
// File:                 <ColourSettingsSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto Saturn>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsSaturn : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsSaturn biomeColourSettingsSaturn;

    // Planet material
    public Material SaturnMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourSaturn;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsSaturn {

        // Public biome array
        public BiomeSaturn[] biomesSaturn;

        // noise settings
        public NoiseSettingsSaturn noiseSaturn;

        // Float for noise offset
        public float SaturnnoiseOffset;

        // Float for noise strength
        public float SaturnnoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float SaturnblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeSaturn {

            // Public Gradient
            public Gradient Saturngradient;

            // Public tint
            public Color Saturntint;

            // Start height of the biome range
            [Range(0,1)]
            public float SaturnstartHeight;

            // Tint percent
            [Range(0, 1)]
            public float SaturntintPercent;

        }

    }

}