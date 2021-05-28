////////////////////////////////////////////////////////////
// File:                 <MinMaxMercury.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Mercury>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxMercury {

    // Public float for min and max, public to get, private to set
    public float MinMercury { get; private set; }
    public float MaxMercury { get; private set; }

    // public MinMax constructor
    public MinMaxMercury() {

        // Min and max values
        MinMercury = float.MaxValue;
        MaxMercury = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxMercury) {

            // Max value = v
            MaxMercury = v;

        }

        // If v is less than the current min value
        if (v < MinMercury) {

            // Min value = v;
            MinMercury = v;

        }


    }

}