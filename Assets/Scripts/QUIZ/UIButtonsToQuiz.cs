////////////////////////////////////////////////////////////
// File:                 <UIButtonsToQuiz.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/04/2021>
// Brief:                <File responsible for moving scenes with buttons>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/04/2021>
// Last Edit Brief:      <Commenting on the code>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIButtonsToQuiz : MonoBehaviour
{
    
    // go to the quiz
    public void GoToTheQuiz() {

        // load the quiz scene
        SceneManager.LoadScene("QuizScene");

    }
    
    // go to the planets
    public void GoToThePlanets() {

        // load the planets scene
        SceneManager.LoadScene("SampleScene");

    }

    


}
