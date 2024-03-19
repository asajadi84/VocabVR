using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UI.Dialogs;
using UnityEngine.UI;

public class VocabBox : MonoBehaviour
{

    [SerializeField] private string objectName;
    [SerializeField] private string objectNameTranslation;
    [SerializeField] private string objectNameSynonym;
    [SerializeField] private AudioClip objectPronunciation;
    [SerializeField] private AudioClip objectPronunciationUk;
    [SerializeField] private AudioClip objectSynPronunciation;
    [SerializeField] private string objectPhonetics;
    [SerializeField] private string objectPhoneticsUk;
    private Text subtitleText;
    [SerializeField] private int objectId;

    private void Awake()
    {
        subtitleText = GameObject.Find("subtitle").GetComponent<Text>();
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; //or confined
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}
    }

    private void OnMouseDown()
    {
        if (!transform.parent.gameObject.GetComponent<SajadiGameManager>().existingDialog)
        {        
            //Destroy(GameObject.Find());
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;

            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = false;
            transform.parent.gameObject.GetComponent<SajadiGameManager>().canPlayIntro = false;
            
            //game mechanic
            GameObject targetCaption = null;
            switch (objectId)
            {
                case 0:
                    targetCaption = transform.parent.gameObject.GetComponent<SajadiGameManager>().caption1;
                    break;
                case 1:
                    targetCaption = transform.parent.gameObject.GetComponent<SajadiGameManager>().caption2;
                    break;
                case 2:
                    targetCaption = transform.parent.gameObject.GetComponent<SajadiGameManager>().caption3;
                    break;
                case 3:
                    targetCaption = transform.parent.gameObject.GetComponent<SajadiGameManager>().caption4;
                    break;
                case 4:
                    targetCaption = transform.parent.gameObject.GetComponent<SajadiGameManager>().caption5;
                    break;
                case 5:
                    targetCaption = transform.parent.gameObject.GetComponent<SajadiGameManager>().caption6;
                    break;
            }
            
            if (transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[objectId] == false)
            {
                GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(transform.parent.gameObject.GetComponent<SajadiGameManager>().hintClip);
                transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[objectId] = true;
                targetCaption.GetComponent<Text>().color = Color.black;
                targetCaption.GetComponent<Text>().text = objectName;
            }

            uDialog originalDialog = uDialog.NewDialog()
                .SetColorScheme("Green Highlight")
                .SetThemeImageSet(eThemeImageSet.SciFi)
                .SetIcon(eIconType.Information)
                .SetTitleText(" ")
                .SetContentFont(transform.parent.gameObject.GetComponent<SajadiGameManager>().VazirMatnBold)
                .SetButtonFont(transform.parent.gameObject.GetComponent<SajadiGameManager>().VazirMatn)
                .SetButtonFontSize(18)
                .SetButtonSize(200.0f, 70.0f)
                .SetContentText(objectName + "\n" + Fa.faConvert(objectNameTranslation))
                .SetContentFontSize(24)
                .SetHeight(250.0f)
                .SetWidth(400.0f)
                .AddButton(Fa.faConvert("بستن"), (dialog) =>
                {
                    subtitleText.text = "";
                    dialog.Close();
                })
                .AddButton(Fa.faConvert("► US"),
                    () =>
                    {
                        //AudioSource.PlayClipAtPoint(objectPronunciation, Camera.main.transform.position);
                        GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(objectPronunciation);
                        subtitleText.text = objectPhonetics;
                    })
                //transform.position)) // Vector3.zero))
                .AddButton(Fa.faConvert("► UK"),
                    () =>
                    {
                        //AudioSource.PlayClipAtPoint(objectPronunciationUk, Camera.main.transform.position);
                        GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(objectPronunciationUk);
                        subtitleText.text = objectPhoneticsUk;
                    })
                .SetCloseWhenOverlayClicked(true)
                .SetCloseWhenAnyButtonClicked(false)
                .SetDestroyAfterClose(true)
                .SetAllowDraggingViaDialog(true)
                .SetAllowDragging(true)
                .SetAllowDraggingViaTitle(true);
            //.Event_OnClose(() => { return null });
                originalDialog.Event_OnClose.AddListener(UnlockCamera); //LockCursor

                if (objectNameSynonym != "")
                {
                    originalDialog.AddButton(Fa.faConvert("مترادف‌ها"), (dialog) =>
                    {
                        //GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(transform.parent.gameObject.GetComponent<SajadiGameManager>().hintClip);
                        GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(objectSynPronunciation);
                        //AudioSource.PlayClipAtPoint(transform.parent.gameObject.GetComponent<SajadiGameManager>().hintClip, Camera.main.transform.position);
                        //AudioSource.PlayClipAtPoint(objectSynPronunciation, Camera.main.transform.position);
                        subtitleText.text = objectNameSynonym;
                    });
                }
                
            //.SetDimensions(400.0f, 200.0f);
             transform.parent.gameObject.GetComponent<SajadiGameManager>().existingDialog = originalDialog.gameObject;
             //originalDialog.gameObject.tag = "uDialog";
             //;
     
             //Debug.Log("salam az tarafe " + objectName);
        }


    }

    private void UnlockCamera(uDialog u)
    {
        subtitleText.text = "";
        GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().enabled = true;
        transform.parent.gameObject.GetComponent<SajadiGameManager>().canPlayIntro = true;
        
        if (
            transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[0] &&
            transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[1] &&
            transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[2] &&
            transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[3] &&
            transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[4] &&
            transform.parent.gameObject.GetComponent<SajadiGameManager>().situation[5]
            )
        {
            GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(transform.parent.gameObject.GetComponent<SajadiGameManager>().welldone1);
            transform.parent.gameObject.GetComponent<SajadiGameManager>().nextButton.SetActive(true);
        }
    }

    //private void LockCursor(uDialog u)
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = true;
    //}
}
