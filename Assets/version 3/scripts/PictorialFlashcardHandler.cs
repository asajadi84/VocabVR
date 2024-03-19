using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PictorialFlashcardHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    private void OnMouseDrag()
    {
        throw new NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //dropped on the right picture
        if (eventData.pointerDrag.transform.GetChild(0).gameObject.GetComponent<Text>().text == gameObject.GetComponent<Image>().sprite.name)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(
                Camera.main.GetComponent<Task2GameManager>().correctAnswerClip);
            
            //destroy the dropped text flashcard
            Destroy(eventData.pointerDrag.transform.gameObject);
            //then destroy the pictorial flashcard itself as well
            Destroy(gameObject);
            //gameObject.SetActive(false);

            Camera.main.GetComponent<Task2GameManager>().solvedCounter++;
            Camera.main.GetComponent<Task2GameManager>().rightContainerSolvedText.text = "Solved: " + Camera.main.GetComponent<Task2GameManager>().solvedCounter + "/20";

            if (Camera.main.GetComponent<Task2GameManager>().solvedCounter == 20)
            {
                //you won
                Camera.main.GetComponent<AudioSource>().PlayOneShot(
                    Camera.main.GetComponent<Task2GameManager>().wonClip);

                float totalScore = ((PlayerPrefs.GetInt("tempScore", 0)+(15 + Camera.main.GetComponent<Task2GameManager>().livesLeft))+0.0f)/2;
                
                //get current session, since it's always an even number it is possible to divide it by two and store it in an int
                int currentSession = SceneManager.GetActiveScene().buildIndex / 2;

                //set the high score
                if (totalScore > PlayerPrefs.GetFloat("scoreSession" + currentSession, 0))
                {
                    PlayerPrefs.SetFloat("scoreSession" + currentSession, totalScore);
                }

                Camera.main.GetComponent<Task2GameManager>().wonPanelScoreText.text =
                    PlayerPrefs.GetInt("tempScore", 0) + Fa.faConvert("نمره بخش اول : ") + "\n" +
                    (15 + Camera.main.GetComponent<Task2GameManager>().livesLeft) + Fa.faConvert("نمره بخش دوم : ") + "\n" +
                    totalScore + Fa.faConvert("نمره کل : ");

                Camera.main.GetComponent<Task2GameManager>().wonPanel.SetActive(true);
            }
        }
        else
        {

            if (Camera.main.GetComponent<Task2GameManager>().livesLeft < 2)
            {
                //you lost
                Camera.main.GetComponent<AudioSource>().PlayOneShot(
                    Camera.main.GetComponent<Task2GameManager>().lostClip);
                Camera.main.GetComponent<Task2GameManager>().heartObjects[0].sprite =
                    Camera.main.GetComponent<Task2GameManager>().heartOffSprite;
                
                Camera.main.GetComponent<Task2GameManager>().lostPanel.SetActive(true);
            }
            else
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(
                    Camera.main.GetComponent<Task2GameManager>().WrongAnswerClip);
                
                Camera.main.GetComponent<Task2GameManager>().livesLeft--;
                Camera.main.GetComponent<Task2GameManager>().heartObjects[
                    Camera.main.GetComponent<Task2GameManager>().livesLeft
                ].sprite = Camera.main.GetComponent<Task2GameManager>().heartOffSprite;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector3.one;
    }
}
