using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WordObjectHandler : MonoBehaviour
{
    [SerializeField] private int wordObjectId;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsPointerOverUIObject() {
        var touchPosition = Touchscreen.current.position.ReadValue();
        var eventData = new PointerEventData(EventSystem.current) { position = touchPosition };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        Debug.Log(results.Count);
        return results.Count > 1;
    }

    private void OnMouseDown()
    {
        if (IsPointerOverUIObject()) { return; }

        if (GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canOpenVocabBox &&
            !GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().hoveredOverPause)
        {
            //Clicked on the right item
            if (wordObjectId == GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().currentVocab)
            {

                //disable everything
                GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canPlayIntro = false;
                GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canOpenVocabBox = false;
                GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = false;

                GameObject.Find("UI_Virtual_Joystick_Move").GetComponent<UIVirtualJoystick>().enabled = false;
                GameObject.Find("UI_Virtual_Joystick_Look").GetComponent<UIVirtualJoystick>().enabled = false;

                GameObject.Find("Vocab Items").GetComponent<AudioSource>().PlayOneShot(
                    GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().correctAnswerClip);
                    
                GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().showVocabBox();
                
                GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().rightContainerFoundText.text =
                    "Found: " + (GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().currentVocab + 1) + "/20";

                Invoke("SetPrnBtnsInteractable", 0.5f);
            }
            //clicked on the wrong item
            else
            {
                //Still can lose more hearts
                if (GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().livesLeft > 1)
                {
                    GameObject.Find("Vocab Items").GetComponent<AudioSource>().PlayOneShot(
                        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().WrongAnswerClip);
                    
                    GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().livesLeft--;
                    GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().heartObjects[
                        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().livesLeft
                    ].sprite = GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().heartOffSprite;
                }
                else
                {
                    //disable everything
                    GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canPlayIntro = false;
                    GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canOpenVocabBox = false;
                    GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = false;
                    //then, show the lost panel
                    GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().heartObjects[0].sprite =
                        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().heartOffSprite;
                    
                    GameObject.Find("Vocab Items").GetComponent<AudioSource>().PlayOneShot(
                        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().lostClip);
                    
                    GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().lostPanel.SetActive(true);
                }
            }
        }
    }

    private void SetPrnBtnsInteractable() {
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().ameButton.interactable = true;
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().breButton.interactable = true;
    }
}
