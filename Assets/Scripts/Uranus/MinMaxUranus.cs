////////////////////////////////////////////////////////////
// File:                 <MinMaxUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Uranus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxUranus {

    // Public float for min and max, public to get, private to set
    public float MinUranus { get; private set; }
    public float MaxUranus { get; private set; }

    // public MinMax constructor
    public MinMaxUranus() {

        // Min and max values
        MinUranus = float.MaxValue;
        MaxUranus = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxUranus) {

            // Max value = v
            MaxUranus = v;

        }

        // If v is less than the current min value
        if (v < MinUranus) {

            // Min value = v;
            MinUranus = v;

        }


    }

}