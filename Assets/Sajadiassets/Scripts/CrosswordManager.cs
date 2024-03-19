using System;
using System.Collections;
using System.Collections.Generic;
using UI.Dialogs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrosswordManager : MonoBehaviour
{
    [SerializeField] private Font VazirMatn;
    [SerializeField] private Font VazirMatnBold;
    
    public bool canInteract = true;

    private bool allCorrect = true;
    private CrossunitHandler[] allCrossUnits;
    
    [SerializeField] private AudioClip crosswordInstructions;
    [SerializeField] private AudioClip crosswordSuccess;
    [SerializeField] private AudioClip crosswordFailure;
    [SerializeField] private AudioClip levelSuccessMusic;

    private bool finalWon = false;

    private void Awake()
    {
        allCrossUnits = GameObject.FindObjectsOfType<CrossunitHandler>();
    }

    private void Start()
    {
        StartCoroutine(narration());
    }

    private void Update()
    {
        if (finalWon)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void validateCrossword()
    {
        StartCoroutine(validate());
    }

    IEnumerator narration()
    {
        yield return new WaitForSeconds(1.5f);
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(crosswordInstructions);
    }

    IEnumerator validate()
    {
        allCorrect = true;
        
        //show results
        canInteract = false;
        foreach (CrossunitHandler unit in allCrossUnits)
        {
            unit.checkAnswer();

            if (!unit.answerIsCorrect)
            {
                allCorrect = false;
            }
        }

        if (allCorrect)
        {
            //Debug.Log("you won");
            
            transform.gameObject.GetComponent<AudioSource>().PlayOneShot(levelSuccessMusic);
            yield return new WaitForSeconds(levelSuccessMusic.length);
            
            //wait for a moment
            yield return new WaitForSeconds(1.0f);
            transform.gameObject.GetComponent<AudioSource>().PlayOneShot(crosswordSuccess);
            finalWon = true;
            
            uDialog originalDialog = uDialog.NewDialog()
                .SetColorScheme("Green Highlight")
                .SetThemeImageSet(eThemeImageSet.SciFi)
                .SetIcon(eIconType.Information)
                .SetTitleText("Well done!")
                .SetContentFont(VazirMatnBold)
                .SetButtonFont(VazirMatn)
                .SetButtonFontSize(18)
                .SetButtonSize(150.0f, 70.0f)
                .SetContentText(Fa.faConvert("آفرین! شما با موفقیت توانستید جدول را حل نمایید.\nبرای بازگشت به صفحه اصلی کلید ESC را فشار دهید."))
                .SetContentFontSize(12)
                .SetHeight(200.0f)
                .SetWidth(500.0f)
                .AddButton(Fa.faConvert("بستن"), (dialog) => dialog.Close())
                .SetCloseWhenOverlayClicked(true)
                .SetCloseWhenAnyButtonClicked(false)
                .SetDestroyAfterClose(true)
                .SetAllowDraggingViaDialog(true)
                .SetAllowDragging(true)
                .SetAllowDraggingViaTitle(true);
        }
        else
        {
            //Debug.Log("try again");
            
            transform.gameObject.GetComponent<AudioSource>().PlayOneShot(crosswordFailure);
            //wait while the error message is getting played
            yield return new WaitForSeconds(crosswordFailure.length);
            
            //hide the results
            foreach (CrossunitHandler unit in allCrossUnits)
            {
                unit.resetAnswer();
            }
            canInteract = true;
        }
        
    }
}