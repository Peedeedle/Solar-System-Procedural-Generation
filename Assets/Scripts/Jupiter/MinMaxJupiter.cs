////////////////////////////////////////////////////////////
// File:                 <MinMaxJupiter.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Jupiter>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxJupiter
{

    // Public float for min and max, public to get, private to set
    public float MinJupiter { get; private set; }
    public float MaxJupiter { get; private set; }

    // public MinMax constructor
    public MinMaxJupiter() {

        // Min and max values
        MinJupiter = float.MaxValue;
        MaxJupiter = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxJupiter) {

            // Max value = v
            MaxJupiter = v;

        }

        // If v is less than the current min value
        if (v < MinJupiter) {

            // Min value = v;
            MinJupiter = v;

        }


    }

}