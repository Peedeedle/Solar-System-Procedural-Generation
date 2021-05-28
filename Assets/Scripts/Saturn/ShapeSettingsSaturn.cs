////////////////////////////////////////////////////////////
// File:                 <ShapeSettingsSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the shape settings while using noise settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettingsSaturn : ScriptableObject {

    // Planet radius float
    public float planetRadius = 1;

    // noise layers
    public NoiseLayerSaturn[] noiseLayersSaturn;

    // Noise layer class
    [System.Serializable]
    public class NoiseLayerSaturn {

        // bool which is called enabled
        public bool enabled = true;

        // If the mountain should use first layer as mask
        public bool useFirstLayerAsMask;

        // noise settings reference
        public NoiseSettingsSaturn noiseSettingsSaturn;

    }


}
