////////////////////////////////////////////////////////////
// File:                 <MinMaxMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for the Moon>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxMoon {

    // Public float for min and max, public to get, private to set
    public float MinMoon { get; private set; }
    public float MaxMoon { get; private set; }

    // public MinMax constructor
    public MinMaxMoon() {

        // Min and max values
        MinMoon = float.MaxValue;
        MaxMoon = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxMoon) {

            // Max value = v
            MaxMoon = v;

        }

        // If v is less than the current min value
        if (v < MinMoon) {

            // Min value = v;
            MinMoon = v;

        }


    }

}