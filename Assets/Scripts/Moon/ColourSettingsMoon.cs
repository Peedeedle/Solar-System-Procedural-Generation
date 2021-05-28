////////////////////////////////////////////////////////////
// File:                 <ColourSettingsMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto the Moon>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsMoon : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsMoon biomeColourSettingsMoon;

    // Planet material
    public Material MoonMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourMoon;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsMoon {

        // Public biome array
        public BiomeMoon[] biomesMoon;

        // noise settings
        public NoiseSettingsMoon noiseMoon;

        // Float for noise offset
        public float MoonnoiseOffset;

        // Float for noise strength
        public float MoonnoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float MoonblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeMoon {

            // Public Gradient
            public Gradient Moongradient;

            // Public tint
            public Color Moontint;

            // Start height of the biome range
            [Range(0,1)]
            public float MoonstartHeight;

            // Tint percent
            [Range(0, 1)]
            public float MoontintPercent;

        }

    }

}