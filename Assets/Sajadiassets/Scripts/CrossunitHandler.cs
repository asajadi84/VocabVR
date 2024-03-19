using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrossunitHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler, IPointerClickHandler
{

    [SerializeField] private Image unitStroke;
    [SerializeField] private Text unitLetter;
    private bool setLetterMode = false;
    private bool hovered = false;
    
    private string[] lettersArray = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    [SerializeField] private int currentPosition = 0;
    [SerializeField] private int correctAnswer;

    public bool answerIsCorrect = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameObject.Find("tefl-crossword-bg").GetComponent<CrosswordManager>().canInteract)
        {
            unitStroke.color = Color.green;
            hovered = true;

            if (!setLetterMode)
            {
                unitLetter.text = "A";
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameObject.Find("tefl-crossword-bg").GetComponent<CrosswordManager>().canInteract)
        {
            unitStroke.color = Color.black;
            hovered = false;
        
            if (!setLetterMode)
            {
                unitLetter.text = "";
            }
        }
    }
    
    public void OnScroll(PointerEventData eventData)
    {
        if (GameObject.Find("tefl-crossword-bg").GetComponent<CrosswordManager>().canInteract)
        {
            setLetterMode = true;
        
            if (eventData.scrollDelta.y > 0.0f)
            {
                //Debug.Log("Up");
                if (currentPosition < 25)
                {
                    currentPosition++;
                }
                else
                {
                    currentPosition = 0;
                }
                unitLetter.text = lettersArray[currentPosition];
            }
            else if(eventData.scrollDelta.y < 0.0f)
            {
                //Debug.Log("Down");
                if (currentPosition > 0)
                {
                    currentPosition--;
                }
                else
                {
                    currentPosition = 25;
                }
                unitLetter.text = lettersArray[currentPosition];
            }
        
            if (currentPosition == correctAnswer)
            {
                answerIsCorrect = true;
            }
            else
            {
                answerIsCorrect = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.Find("tefl-crossword-bg").GetComponent<CrosswordManager>().canInteract)
        {
            if (!setLetterMode)
            {
                setLetterMode = true;
                unitLetter.text = "A";
            
                if (currentPosition == correctAnswer)
                {
                    answerIsCorrect = true;
                }
                else
                {
                    answerIsCorrect = false;
                }
            }
        }
    }

    private void Update()
    {
        if (GameObject.Find("tefl-crossword-bg").GetComponent<CrosswordManager>().canInteract)
        {
            if (hovered)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    setLetterMode = true;
                    if (currentPosition < 25)
                    {
                        currentPosition++;
                    }
                    else
                    {
                        currentPosition = 0;
                    }
                    unitLetter.text = lettersArray[currentPosition];
                
                    if (currentPosition == correctAnswer)
                    {
                        answerIsCorrect = true;
                    }
                    else
                    {
                        answerIsCorrect = false;
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    setLetterMode = true;
                    if (currentPosition > 0)
                    {
                        currentPosition--;
                    }
                    else
                    {
                        currentPosition = 25;
                    }
                    unitLetter.text = lettersArray[currentPosition];
                
                    if (currentPosition == correctAnswer)
                    {
                        answerIsCorrect = true;
                    }
                    else
                    {
                        answerIsCorrect = false;
                    }
                }
            }
        }
    }

    public void checkAnswer()
    {
        //note that it must be "answerIsCorrect" and not direct comparison of currentPosition and correctAnswer
        //since the currentPosition has a default value of 0
        if (answerIsCorrect)
        {
            unitLetter.color = new Color32(0, 128, 38, 255);
        }
        else
        {
            unitLetter.color = Color.red;
        }
    }

    public void resetAnswer()
    {
        unitLetter.color = Color.black;
    }
}
