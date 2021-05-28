////////////////////////////////////////////////////////////
// File:                 <MinMax.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for the planet>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax {

    // Public float for min and max, public to get, private to set
    public float Min { get; private set; }
    public float Max { get; private set; }

    // public MinMax constructor
    public MinMax() {

        // Min and max values
        Min = float.MaxValue;
        Max = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > Max) {

            // Max value = v
            Max = v;

        }

        // If v is less than the current min value
        if (v < Min) {

            // Min value = v;
            Min = v;

        }


    }

}