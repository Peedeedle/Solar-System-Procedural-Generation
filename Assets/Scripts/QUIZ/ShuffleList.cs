////////////////////////////////////////////////////////////
// File:                 <ShuffleList.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/04/2021>
// Brief:                <File responsible for shuffling each question>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/04/2021>
// Last Edit Brief:      <Commenting on the code>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShuffleList {

    // static for shufffling list items
    public static List<E> ShuffleListItems<E>(List<E> inputList) {
        List<E> originalList = new List<E>();
        originalList.AddRange(inputList);
        List<E> randomList = new List<E>();

        // random index
        System.Random r = new System.Random();
        int randomIndex = 0;

        // while list is more than 0
        while (originalList.Count > 0) {
            randomIndex = r.Next(0, originalList.Count); //Choose a random object in the list
            randomList.Add(originalList[randomIndex]); //add it to the new, random list
            originalList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }
}