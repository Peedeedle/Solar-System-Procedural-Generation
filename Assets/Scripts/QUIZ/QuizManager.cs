////////////////////////////////////////////////////////////
// File:                 <QuizManager.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/04/2021>
// Brief:                <File responsible for managing all quiz aspects>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/04/2021>
// Last Edit Brief:      <Commenting on the code>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{

    // Text for the correct answers
    public Text NumberOfCorrectAnswersText;

    // Current correct answers
    public int CurrentNumberOfCorrectAnswers;

    // QuizUI script reference
    [SerializeField] private QuizUI quizUI;

    // Quiz Data reference
    [SerializeField] private List<QuizDataScriptable> quizData;

    // Private list of questions
    private List<Question> questions;

    // Selected question
    private Question selectedQuestion;

    // Game over panel
    public GameObject GameOverMenuPanel;

    // On start
    public void Start() {

        // start the game
        StartGame(0);

    }

    // Update
    void Update() {


        // If current score is = 0
        if (CurrentNumberOfCorrectAnswers == 0) {

            // Show the number of answers the player got out of max
            NumberOfCorrectAnswersText.text = "Congratulations, you got" + " " + CurrentNumberOfCorrectAnswers + " " + "questions correct out of 41";

        }

        // If current score is = 1
        if (CurrentNumberOfCorrectAnswers == 1) {

            // Show the number of answers the player got out of max
            NumberOfCorrectAnswersText.text = "Congratulations, you got" + " " + CurrentNumberOfCorrectAnswers + " " + "question correct out of 41";

        }

        // If current score is more than or = 2
        if (CurrentNumberOfCorrectAnswers >= 2) {

            // Show the number of answers the player got out of max
            NumberOfCorrectAnswersText.text = "Congratulations, you got" + " " + CurrentNumberOfCorrectAnswers + " " + "questions correct out of 41";

        }

        

    }

    // Start is called before the first frame update
    public void StartGame(int index)
    {

        // Set correct answers to 0
        CurrentNumberOfCorrectAnswers = 0;

        // questions = new list of Questions
        questions = new List<Question>();

        // for each quizdata index with the question count
        for (int i = 0; i < quizData[index].questions.Count; i++) {

            // Set the questions to the scriptable objects question data
            questions.Add(quizData[index].questions[i]);

        }

        

        // Select a question
        SelectQuestion();
        
    }

    // Select Question
    void SelectQuestion() {

        // val Int = random range between 0 and questions count
        int val = Random.Range(0, questions.Count);

        // Selected question = question val
        selectedQuestion = questions[val];

        // Set the question as the selected question
        quizUI.SetQuestion(selectedQuestion);

        // Remove already asked question
        questions.RemoveAt(val);

    }

    // Check the answer against the string of answered questions
    public bool Answer(string Answered) {

        // set answered to false
        bool correctAnswer = false;

        // if answered bool is true and = correct answer
        if (Answered == selectedQuestion.correctAns) {

            //  correct answer = true
            correctAnswer = true;

            // current number of correct answers + 1
            CurrentNumberOfCorrectAnswers += 1;

        } else {

            // do nothing


        }

        // set the correct answer text to number of correct answers
        NumberOfCorrectAnswersText.text = CurrentNumberOfCorrectAnswers.ToString();

        // Check to see if there is any questions remaining
        if (questions.Count > 0) {

            // Invoke the next question after half a second
            Invoke("SelectQuestion", 0.5f);


        } else {

            // Set the game over menu panel to true
            GameOverMenuPanel.SetActive(true);

        }

        

        // return the correct answer
        return correctAnswer;

    }

    

}

[System.Serializable]
// class for the questions
public class Question {

    // String for question info
    public string questionInfo;

    // Public question type
    public QuestionType questionType;

    // Sprite for the question image
    public Sprite questionImage;
    
    // list for the string options
    public List<string> options;

    // String for the correct answers
    public string correctAns;


}

[System.Serializable]
// Enum called question type
public enum QuestionType {

    // Two question types
    TEXT,
    IMAGE
}