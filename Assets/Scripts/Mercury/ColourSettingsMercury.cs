////////////////////////////////////////////////////////////
// File:                 <ColourSettingsMercury.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto Mercury>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsMercury : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsMercury biomeColourSettingsMercury;

    // Planet material
    public Material MercuryMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourMercury;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsMercury {

        // Public biome array
        public BiomeMercury[] biomesMercury;

        // noise settings
        public NoiseSettingsMercury noiseMercury;

        // Float for noise offset
        public float MercurynoiseOffset;

        // Float for noise strength
        public float MercurynoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float MercuryblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomeMercury {

            // Public Gradient
            public Gradient Mercurygradient;

            // Public tint
            public Color Mercurytint;

            // Start height of the biome range
            [Range(0,1)]
            public float MercurystartHeight;

            // Tint percent
            [Range(0, 1)]
            public float MercurytintPercent;

        }

    }

}