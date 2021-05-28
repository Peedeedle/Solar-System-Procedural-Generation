////////////////////////////////////////////////////////////
// File:                 <MinMaxMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Mars>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxMars {

    // Public float for min and max, public to get, private to set
    public float MinMars { get; private set; }
    public float MaxMars { get; private set; }

    // public MinMax constructor
    public MinMaxMars() {

        // Min and max values
        MinMars = float.MaxValue;
        MaxMars = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxMars) {

            // Max value = v
            MaxMars = v;

        }

        // If v is less than the current min value
        if (v < MinMars) {

            // Min value = v;
            MinMars = v;

        }


    }

}