////////////////////////////////////////////////////////////
// File:                 <QuizUI.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/04/2021>
// Brief:                <File responsible for managing all quiz UI aspects>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/04/2021>
// Last Edit Brief:      <Commenting on the code>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    // Quiz Manager reference
    [SerializeField] private QuizManager quizManager;

    // Text for question 
    [SerializeField] private Text questionText;

    // Image for question image
    [SerializeField] private Image questionImage;

    // Options / Buttons list
    [SerializeField] private List<Button> options;

    // Colours for the buttons to show player the outcome
    [SerializeField] private Color correctColour, wrongColour, normalColour;

    // question
    private Question question;

    // bool for if the question has been answered
    private bool Answered;
    

    

    // Start is called before the first frame update
    void Start()
    {
        
        // For each button
        for (int i = 0; i < options.Count; i++) {

            // each button is a local button with options [i]
            Button Localbutton = options[i];
            
            // Add a listener on the button to know when the button is pressed, do OnClick()
            Localbutton.onClick.AddListener(() => OnClick(Localbutton));

            
        }

    }

    // Set the question
    public void SetQuestion(Question question) {

        // this question = question 
        this.question = question;

        // Depending on the question type
        switch (question.questionType) {


            // Question type text
            case QuestionType.TEXT:

                // Set the parent gameobject to false
                questionImage.transform.gameObject.SetActive(false);


                // Break the code
                break;



            // Question type Image
            case QuestionType.IMAGE:

                // Run the image holder void
                ImageHolder();

                // Set the game object to false
                questionImage.transform.gameObject.SetActive(true);

                // Question image sprite = question image
                questionImage.sprite = question.questionImage; 

                // Break the code
                break;
        }

        // Question text = question text on manager
        questionText.text = question.questionInfo;

        // Shuffle the question
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        // Set the button text to the name of the option
        for (int i = 0; i < options.Count; i++) {

            // Set the text in buttons to answer list
            options[i].GetComponentInChildren<Text>().text = answerList[i];

            // set name of buttons to the answer list
            options[i].name = answerList[i];

            // Set options to normal colour
            options[i].image.color = normalColour;
            
        }

        // Set answered to false
        Answered = false;

    }

    // Image holder 
    void ImageHolder() {

        // Set the parent gameobject to true
        questionImage.transform.parent.gameObject.SetActive(true);

        // Set the game object to false
        questionImage.transform.gameObject.SetActive(false);

    }

    // When a button is pressed
    public void OnClick(Button button) {
        
        // if answered is = false
        if (!Answered) {
            
            // Set answered to true
            Answered = true;

            // Check to see if the button name is correct
            bool val = quizManager.Answer(button.name);

            // If value is true
            if (val) {

                // Set the button image to the correct colour
                button.image.color = correctColour;

            // If the answer was wrong
            } else {

                // Set the button image to the wrong colour
                button.image.color = wrongColour;

            }

        }
        

    }
    
}
