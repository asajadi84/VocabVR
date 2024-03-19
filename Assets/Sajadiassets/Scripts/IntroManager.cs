using System;
using System.Collections;
using System.Collections.Generic;
using UI.Dialogs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

    [SerializeField] private AudioClip introClip;

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playClip());
        StartCoroutine(fadeInBanner());
    }
    
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene(0);
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //fade in banner
        //if (gameObject.GetComponent<SpriteRenderer>().color.a < 1.0f)
        //{
            
        //}
    }

    private void OnMouseDown()
    {
        if (GameObject.Find("Vocab Items").GetComponent<SajadiGameManager>().canPlayIntro)
        {
            //AudioSource.PlayClipAtPoint(introClip, Vector3.zero);
            GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(introClip);
        }
    }

    IEnumerator playClip()
    {
        yield return new WaitForSeconds(1.0f);
        //AudioSource.PlayClipAtPoint(introClip, Vector3.zero);
        GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(introClip);
        yield return new WaitForSeconds(introClip.length);
        
        uDialog introDialog = uDialog.NewDialog()
            .SetColorScheme("Green Highlight")
            .SetThemeImageSet(eThemeImageSet.SciFi)
            .SetIcon(eIconType.Information)
            .SetTitleText(Fa.faConvert("راهنمای بازی"))
            .SetContentFont(GameObject.Find("Vocab Items").GetComponent<SajadiGameManager>().VazirMatnBold)
            .SetButtonFont(GameObject.Find("Vocab Items").GetComponent<SajadiGameManager>().VazirMatn)
            .SetTitleFont(GameObject.Find("Vocab Items").GetComponent<SajadiGameManager>().VazirMatnBold)
            .SetButtonFontSize(18)
            .SetButtonSize(150.0f, 70.0f)
            .SetContentText(Fa.faConvert("برای چرخش در محیط ماوس را حرکت دهید.\nبرای جابجایی در محیط از کلیدهای جهت‌نمای کیبورد استفاده کنید.\nاشیای بالای صفحه را در محیط پیدا کرده و روی آن‌ها کلیک کنید."))
            .SetContentFontSize(12)
            .SetHeight(300.0f)
            .SetWidth(500.0f)
            .AddButton(Fa.faConvert("بستن"), (dialog) => dialog.Close())
            .SetCloseWhenOverlayClicked(true)
            .SetCloseWhenAnyButtonClicked(false)
            .SetDestroyAfterClose(true)
            .SetAllowDraggingViaDialog(true)
            .SetAllowDragging(true)
            .SetAllowDraggingViaTitle(true);
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
