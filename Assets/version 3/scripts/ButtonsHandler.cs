using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{
    [SerializeField] private GameObject vocabItems;
    [SerializeField] private GameObject introductionPanel;
    [SerializeField] private GameObject wordBox;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Image containerImage;
    [SerializeField] private Text containerText;
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private TransitionSettings mainSceneTransition;
    [SerializeField] private TransitionSettings nextSceneTransition;
    [SerializeField] private TransitionSettings uiTransition;

    public void playAmEPronunciation()
    {
        vocabItems.GetComponent<AudioSource>().PlayOneShot(
            vocabItems.GetComponent<Task1GameManager>().vocabPronunciationUS[
                vocabItems.GetComponent<Task1GameManager>().currentVocab
            ]
            );
    }
    
    public void playBrEPronunciation()
    {
        vocabItems.GetComponent<AudioSource>().PlayOneShot(
            vocabItems.GetComponent<Task1GameManager>().vocabPronunciationUK[
                vocabItems.GetComponent<Task1GameManager>().currentVocab
            ]
        );
    }
    
    public void closeIntroductionPanel()
    {
        //we are in task 1
        if (vocabItems)
        {
            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = true;
            vocabItems.GetComponent<Task1GameManager>().canPlayIntro = true;
            vocabItems.GetComponent<Task1GameManager>().canOpenVocabBox = true;
            vocabItems.GetComponent<AudioSource>().Stop();

            GameObject.Find("UI_Virtual_Joystick_Move").GetComponent<UIVirtualJoystick>().enabled = true;
            GameObject.Find("UI_Virtual_Joystick_Look").GetComponent<UIVirtualJoystick>().enabled = true;
        }
        //task 2
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        }

        introductionPanel.SetActive(false);
    }

    public void closeWordbox()
    {
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().ameButton.interactable = false;
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().breButton.interactable = false;

        if (vocabItems.GetComponent<Task1GameManager>().currentVocab < 19)
        {
            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = true;
            vocabItems.GetComponent<Task1GameManager>().canPlayIntro = true;
            vocabItems.GetComponent<Task1GameManager>().canOpenVocabBox = true;
            wordBox.SetActive(false);

            vocabItems.GetComponent<Task1GameManager>().currentVocab++;
            containerImage.sprite = vocabItems.GetComponent<Task1GameManager>().vocabSprites[
                vocabItems.GetComponent<Task1GameManager>().currentVocab];
            containerText.text = Fa.faConvert(vocabItems.GetComponent<Task1GameManager>().vocabTranslation[
                vocabItems.GetComponent<Task1GameManager>().currentVocab]);
        }
        else
        {
            //set the tempScore for the next stage
            PlayerPrefs.SetInt("tempScore", 15 + vocabItems.GetComponent<Task1GameManager>().livesLeft);
            loadNextScene();
        }

        GameObject.Find("UI_Virtual_Joystick_Move").GetComponent<UIVirtualJoystick>().enabled = true;
        GameObject.Find("UI_Virtual_Joystick_Look").GetComponent<UIVirtualJoystick>().enabled = true;

    }
    
    public void pauseGame()
    {
        //we are in task 1
        if (vocabItems)
        {
            if (GameObject.Find("Welcome Banner").GetComponent<WelcomeManager>().HoldIntroPlay != null)
            {
                StopCoroutine(GameObject.Find("Welcome Banner").GetComponent<WelcomeManager>().HoldIntroPlay);
            }

            vocabItems.GetComponent<Task1GameManager>().canPlayIntro = false;
            vocabItems.GetComponent<Task1GameManager>().canOpenVocabBox = false;
            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = false;

            GameObject.Find("UI_Virtual_Joystick_Move").GetComponent<UIVirtualJoystick>().enabled = false;
            GameObject.Find("UI_Virtual_Joystick_Look").GetComponent<UIVirtualJoystick>().enabled = false;
        }

        pausePanel.SetActive(true);
    }

    public void pauseBtnEnter()
    {
        vocabItems.GetComponent<Task1GameManager>().hoveredOverPause = true;
    }
    
    public void pauseBtnExit()
    {
        vocabItems.GetComponent<Task1GameManager>().hoveredOverPause = false;
    }

    public void resumeGame()
    {
        //we are in task 1
        if (vocabItems)
        {
            vocabItems.GetComponent<Task1GameManager>().canPlayIntro = true;
            vocabItems.GetComponent<Task1GameManager>().canOpenVocabBox = true;
            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = true;

            GameObject.Find("UI_Virtual_Joystick_Move").GetComponent<UIVirtualJoystick>().enabled = true;
            GameObject.Find("UI_Virtual_Joystick_Look").GetComponent<UIVirtualJoystick>().enabled = true;
        }
        
        pausePanel.SetActive(false);
    }

    public void showGuidePanelFromPause()
    {
        pausePanel.SetActive(false);
        introductionPanel.SetActive(true);
        
        //task1
        if (vocabItems)
        {
            vocabItems.GetComponent<AudioSource>().Stop();
            vocabItems.GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Welcome Banner").GetComponent<WelcomeManager>().task1IntroClip);
        }
        //task2
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Main Camera").GetComponent<Task2GameManager>().task2IntroClip);
        }


    }

    public void showAboutPanel()
    {
        aboutPanel.SetActive(true);
    }

    public void hideAboutPanel()
    {
        aboutPanel.SetActive(false);
        //aboutPanel.GetComponent<Image>().DOFade(0, 2.0f);
    }

    public void openMarketReview() {
        // Application.OpenURL("://comment?id=com.SajadiDev.VocabVR");

        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_EDIT"));
        intentObject.Call<AndroidJavaObject>("setData", uriClass.CallStatic<AndroidJavaObject>("parse", "bazaar://details?id=com.SajadiDev.VocabVR"));
        intentObject.Call<AndroidJavaObject>("setPackage", "com.farsitel.bazaar");

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intentObject);
    }

    public void exitApplication()
    {
        Application.Quit();
    }

    public void loadCurrentScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex, mainSceneTransition, 0.0f);
    }
    
    public void loadPreviousScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex - 1, mainSceneTransition, 0.0f);
    }
    
    public void loadNextScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex + 1, nextSceneTransition, 0.0f);
    }
    
    public void loadMainScene()
    {
        //SceneManager.LoadScene(0);
        TransitionManager.Instance().Transition(0, mainSceneTransition, 0.0f);
    }
    
    public void loadCustomScene(int sessionIndex)
    {
        int sessionToSceneId = (sessionIndex * 2) - 1;
        //SceneManager.LoadScene(sessionToSceneId);
        TransitionManager.Instance().Transition(sessionToSceneId, mainSceneTransition, 0.0f);
        //Debug.Log(sessionToSceneId);
    }
}
