////////////////////////////////////////////////////////////
// File:                 <ColourSettingsPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the colour and biome settings onto Pluto>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettingsPluto : ScriptableObject {

    // Public biome colour settings
    public BiomeColourSettingsPluto biomeColourSettingsPluto;

    // Planet material
    public Material PlutoMaterial;

    // Gradient for the ocean colour
    public Gradient oceanColourPluto;

    // Biome colour settings
    [System.Serializable]
    public class BiomeColourSettingsPluto
    {

        // Public biome array
        public BiomePluto[] biomesPluto;

        // noise settings
        public NoiseSettingsPluto noisePluto;

        // Float for noise offset
        public float PlutonoiseOffset;

        // Float for noise strength
        public float PlutonoiseStrength;

        // Blend amount for colour of biomes
        [Range(0,1)]
        public float PlutoblendAmount;

        //Biome class
        [System.Serializable]
        public class BiomePluto
        {

            // Public Gradient
            public Gradient Plutogradient;

            // Public tint
            public Color Plutotint;

            // Start height of the biome range
            [Range(0,1)]
            public float PlutostartHeight;

            // Tint percent
            [Range(0, 1)]
            public float PlutotintPercent;

        }

    }

}