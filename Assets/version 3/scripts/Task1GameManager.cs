using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task1GameManager : MonoBehaviour
{

    [Header("State Variables")]
    public int currentVocab = 0;
    public bool canPlayIntro = true;
    public bool canOpenVocabBox = true;
    public int livesLeft = 5;
    public Image[] heartObjects;
    public bool hoveredOverPause = false;
    
    [Header("Scene Variables")]
    [SerializeField] private Image vocabClipart;
    [SerializeField] private Text vocabTranslationText;
    public GameObject lostPanel;
    public Text rightContainerFoundText;
    public Image leftContainerClipart;
    public Text leftContainerText;
    [SerializeField] private GameObject vocabBox;
    [SerializeField] private Image vocabBoxClipart;
    [SerializeField] private Text vocabBoxWritten;
    [SerializeField] private Text vocabBoxTranslation;
    [SerializeField] private Text vocabBoxRelated;
    [SerializeField] private Text vocabBoxExample;
    
    [Header("Asset Variables")]
    public Sprite heartOffSprite;
    public AudioClip correctAnswerClip;
    public AudioClip WrongAnswerClip;
    public AudioClip lostClip;

    [Header("Vocabulary Variables")]
    public string[] vocabWritten;
    public string[] vocabTranslation;
    public string[] vocabExample;
    public string[] vocabRelated;
    public AudioClip[] vocabPronunciationUS;
    public AudioClip[] vocabPronunciationUK;
    public Sprite[] vocabSprites;

    public Button ameButton;
    public Button breButton;

    private void Awake()
    {
        //reset the tempScore back to zero
        PlayerPrefs.SetInt("tempScore", 0);
        
        //set the first word properties to the UI left container
        vocabClipart.GetComponent<Image>().sprite = vocabSprites[0];
        vocabTranslationText.text = Fa.faConvert(vocabTranslation[0]);

        ameButton.interactable = false;
        breButton.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showVocabBox()
    {
        vocabBoxClipart.sprite = vocabSprites[currentVocab];
        vocabBoxWritten.text = vocabWritten[currentVocab];
        vocabBoxTranslation.text = Fa.faConvert(vocabTranslation[currentVocab]);
        vocabBoxRelated.text = vocabRelated[currentVocab];
        vocabBoxExample.text = vocabExample[currentVocab];
        
        vocabBox.SetActive(true);
    }
}
