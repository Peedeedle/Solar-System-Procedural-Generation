////////////////////////////////////////////////////////////
// File:                 <ShapeSettingsMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the shape settings while using noise settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettingsMars : ScriptableObject {

    // Planet radius float
    public float planetRadius = 1;

    // noise layers
    public NoiseLayerMars[] noiseLayersMars;

    // Noise layer class
    [System.Serializable]
    public class NoiseLayerMars {

        // bool which is called enabled
        public bool enabled = true;

        // If the mountain should use first layer as mask
        public bool useFirstLayerAsMask;

        // noise settings reference
        public NoiseSettingsMars noiseSettingsMars;

    }


}
