using System.Collections;
using System.Collections.Generic;
using UI.Dialogs;
using UnityEngine;

public class AboutBox : MonoBehaviour
{
    [SerializeField] private Font VazirMatn;
    [SerializeField] private Font VazirMatnBold;

    [SerializeField] private GameObject btn1;
    [SerializeField] private GameObject btn2;
    [SerializeField] private GameObject btn3;
    [SerializeField] private GameObject btn4;
    [SerializeField] private GameObject btn5;
    
    private void OnMouseDown()
    {
        StartCoroutine(showAbout());
    }
    
    private void enableBtns(uDialog u)
    {
        btn1.GetComponent<BoxCollider2D>().enabled = true;
        btn2.GetComponent<BoxCollider2D>().enabled = true;
        btn3.GetComponent<BoxCollider2D>().enabled = true;
        btn4.GetComponent<BoxCollider2D>().enabled = true;
        btn5.GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator showAbout()
    {
        btn1.GetComponent<BoxCollider2D>().enabled = false;
        btn2.GetComponent<BoxCollider2D>().enabled = false;
        btn3.GetComponent<BoxCollider2D>().enabled = false;
        btn4.GetComponent<BoxCollider2D>().enabled = false;
        btn5.GetComponent<BoxCollider2D>().enabled = false;
        
        uDialog originalDialog = uDialog.NewDialog()
            .SetColorScheme("Green Highlight")
            .SetThemeImageSet(eThemeImageSet.SciFi)
            .SetIcon(eIconType.Information)
            .SetTitleText("About Game")
            .SetContentFont(VazirMatnBold)
            .SetButtonFont(VazirMatn)
            .SetButtonFontSize(18)
            .SetButtonSize(150.0f, 70.0f)
            .SetContentText("Supervisor: Dr. Ali Roohani\nDeveloped by: Ali Sajadi\nhttps://sajadidev.ir\n\nAssets, sounds, and phonetics provided by:\nSketchfab - Unity Asset Store - Freepik - TTSFree - Aipaa.ir - Oxford Learner's Dictionaries\nShahrekord University - 2022-2023")
            .SetContentFontSize(12)
            .SetHeight(400.0f)
            .SetWidth(400.0f)
            //.AddButton(Fa.faConvert("بستن"), (dialog) =>
            //{
            //    Debug.Log("test close");
            //    dialog.Close();
            //})
            .SetCloseWhenOverlayClicked(true)
            .SetCloseWhenAnyButtonClicked(false)
            .SetDestroyAfterClose(true)
            .SetAllowDraggingViaDialog(true)
            .SetAllowDragging(true)
            .SetAllowDraggingViaTitle(true);
        //originalDialog.Event_OnClose.AddListener(enableBtns);

        yield return new WaitForSeconds(5.0f);
        
        originalDialog.Close();
        
        btn1.GetComponent<BoxCollider2D>().enabled = true;
        btn2.GetComponent<BoxCollider2D>().enabled = true;
        btn3.GetComponent<BoxCollider2D>().enabled = true;
        btn4.GetComponent<BoxCollider2D>().enabled = true;
        btn5.GetComponent<BoxCollider2D>().enabled = true;
    }
}
