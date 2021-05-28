////////////////////////////////////////////////////////////
// File:                 <MinMaxNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Neptune>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxNeptune {

    // Public float for min and max, public to get, private to set
    public float MinNeptune { get; private set; }
    public float MaxNeptune { get; private set; }

    // public MinMax constructor
    public MinMaxNeptune() {

        // Min and max values
        MinNeptune = float.MaxValue;
        MaxNeptune = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxNeptune) {

            // Max value = v
            MaxNeptune = v;

        }

        // If v is less than the current min value
        if (v < MinNeptune) {

            // Min value = v;
            MinNeptune = v;

        }


    }

}