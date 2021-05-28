////////////////////////////////////////////////////////////
// File:                 <INoiseFilter.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for the INoiseFilter's float interface>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseFilter {

    //
    float Evaluate(Vector3 point);

}