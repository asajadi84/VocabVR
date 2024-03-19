using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task2GameManager : MonoBehaviour
{
    [Header("State Variables")]
    public bool canDragCards = true;
    public int livesLeft = 5;
    public Image[] heartObjects;
    
    [Header("Scene Variables")]
    public GameObject lostPanel;
    public GameObject wonPanel;
    public Text rightContainerSolvedText;
    public Text wonPanelScoreText;
    public int solvedCounter = 0;
    public GameObject introductionPanel;
    
    [Header("Asset Variables")]
    public Sprite heartOffSprite;
    public AudioClip correctAnswerClip;
    public AudioClip WrongAnswerClip;
    public AudioClip lostClip;
    public AudioClip wonClip;
    public AudioClip task2IntroClip;

    private void Awake()
    {
        //set flashcards sprites and texts
        //throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        introductionPanel.SetActive(true);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(task2IntroClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
