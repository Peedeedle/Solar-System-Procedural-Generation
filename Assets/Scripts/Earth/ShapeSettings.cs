////////////////////////////////////////////////////////////
// File:                 <ShapeSettings.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the shape settings while using noise settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject {

    // Planet radius float
    public float planetRadius = 1;

    // noise layers
    public NoiseLayer[] noiseLayers;

    // Noise layer class
    [System.Serializable]
    public class NoiseLayer {

        // bool which is called enabled
        public bool enabled = true;

        // If the mountain should use first layer as mask
        public bool useFirstLayerAsMask;

        // noise settings reference
        public NoiseSettings noiseSettings;

    }


}
