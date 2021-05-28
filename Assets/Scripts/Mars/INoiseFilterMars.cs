////////////////////////////////////////////////////////////
// File:                 <INoiseFilterMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for the INoiseFilter's float interface>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseFilterMars {

    //
    float EvaluateMars(Vector3 point);

}