////////////////////////////////////////////////////////////
// File:                 <MinMaxSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Saturn>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxSaturn {

    // Public float for min and max, public to get, private to set
    public float MinSaturn { get; private set; }
    public float MaxSaturn { get; private set; }

    // public MinMax constructor
    public MinMaxSaturn() {

        // Min and max values
        MinSaturn = float.MaxValue;
        MaxSaturn = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxSaturn) {

            // Max value = v
            MaxSaturn = v;

        }

        // If v is less than the current min value
        if (v < MinSaturn) {

            // Min value = v;
            MinSaturn = v;

        }


    }

}