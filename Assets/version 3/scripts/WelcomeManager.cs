using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WelcomeManager : MonoBehaviour
{
    [SerializeField] private AudioClip greetingClip;
    [SerializeField] private GameObject introductionPanel;
    public AudioClip task1IntroClip;

    public Coroutine HoldIntroPlay;
    
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playClip());
        StartCoroutine(fadeInBanner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsPointerOverUIObject()
    {
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

        if (GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canPlayIntro)
        {
            HoldIntroPlay = StartCoroutine(holdIntroPlay());
            //AudioSource.PlayClipAtPoint(introClip, Vector3.zero);
            GameObject.Find("Vocab Items").GetComponent<AudioSource>().PlayOneShot(greetingClip);
        }
    }
    
    IEnumerator playClip()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Vocab Items").GetComponent<AudioSource>().PlayOneShot(greetingClip);
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canPlayIntro = false;
        yield return new WaitForSeconds(greetingClip.length);

        GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = false;
        introductionPanel.SetActive(true);
        GameObject.Find("Vocab Items").GetComponent<AudioSource>().PlayOneShot(task1IntroClip);
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canOpenVocabBox = false;
    }

    IEnumerator holdIntroPlay()
    {
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canPlayIntro = false;
        yield return new WaitForSeconds(greetingClip.length);
        GameObject.Find("Vocab Items").GetComponent<Task1GameManager>().canPlayIntro = true;
    }

    IEnumerator fadeInBanner()
    {
        for (float i = 0.0f; i < 1.0f; i+=0.02f) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, i);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
