////////////////////////////////////////////////////////////
// File:                 <MinMaxPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the Minimum and Maximum elevation for Pluto>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxPluto
{

    // Public float for min and max, public to get, private to set
    public float MinPluto { get; private set; }
    public float MaxPluto { get; private set; }

    // public MinMax constructor
    public MinMaxPluto() {

        // Min and max values
        MinPluto = float.MaxValue;
        MaxPluto = float.MinValue;

    }

    public void AddValue(float v) {

        // If V is greater than current max value
        if (v > MaxPluto) {

            // Max value = v
            MaxPluto = v;

        }

        // If v is less than the current min value
        if (v < MinPluto) {

            // Min value = v;
            MinPluto = v;

        }


    }

}