////////////////////////////////////////////////////////////
// File:                 <MinMaxVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Venus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxVenus {

    // Public float for min and max, public to get, private to set
    public float MinVenus { get; private set; }
    public float MaxVenus { get; private set; }

    // public MinMax constructor
    public MinMaxVenus() {

        // Min and max values
        MinVenus = float.MaxValue;
        MaxVenus = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxVenus) {

            // Max value = v
            MaxVenus = v;

        }

        // If v is less than the current min value
        if (v < MinVenus) {

            // Min value = v;
            MinVenus = v;

        }


    }

}