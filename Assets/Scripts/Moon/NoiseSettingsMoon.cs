////////////////////////////////////////////////////////////
// File:                 <NoiseSettingsMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the noise settings>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettingsMoon {

    // Simple and Rigig filter types
    public enum FilterTypeMoon { Simple, Rigid};

    // public filter type
    public FilterTypeMoon filterTypeMoon;

    [ConditionalHide("filterTypeMoon", 0)]
    // Simple and rigid noise settings
    public SimpleNoiseSettingsMoon simpleNoiseSettingsMoon;

    [ConditionalHide("filterTypeMoon", 1)]
    public RigidNoiseSettingsMoon rigidNoiseSettingsMoon;

    [System.Serializable]
    // Simple noise settings
    public class SimpleNoiseSettingsMoon {

        // range of layers 1-8
        [Range(1, 8)]
        public int numLayers = 1;

        // public float for noise strength, baseRoughness, roughness, persistence and vector 3 centre
        public float strength = 1;
        public float baseRoughness = 1;
        public float persistence = 0.5f;
        public float roughness = 2;

        public Vector3 centre;

        // float for the minimum value
        public float minValue;

    }

    [System.Serializable]
    // Public rigid noise settings, inherit from simple noise settings
    public class RigidNoiseSettingsMoon : SimpleNoiseSettingsMoon {

        // Weight multiplier
        public float weightMultiplier = 0.8f;

    }

    

}