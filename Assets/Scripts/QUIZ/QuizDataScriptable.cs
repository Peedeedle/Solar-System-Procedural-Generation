////////////////////////////////////////////////////////////
// File:                 <QuizDataScriptable.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/04/2021>
// Brief:                <File responsible for the scriptable object>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/04/2021>
// Last Edit Brief:      <Commenting on the code>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create asset menu with the file and menu names of QuestionData
[CreateAssetMenu(fileName = "QuestionData", menuName = "QuestionData")]
public class QuizDataScriptable : ScriptableObject
{

    // List for questions
    public List<Question> questions;

}
