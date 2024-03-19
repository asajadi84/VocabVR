using System;
using System.Collections;
using System.Collections.Generic;
using UI.Dialogs;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class SajadiGameManager : MonoBehaviour
{
    public Font VazirMatn;
    public Font VazirMatnBold;
    public GameObject existingDialog;
    public AudioClip hintClip;
    public AudioClip welldone1;
    public bool canPlayIntro = true;
    
    public bool[] situation = {false, false, false, false, false, false};

    public GameObject caption1;
    public GameObject caption2;
    public GameObject caption3;
    public GameObject caption4;
    public GameObject caption5;
    public GameObject caption6;

    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
