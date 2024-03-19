using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator logoAnimator;
    [SerializeField] private Animator blackboardAnimator;
    [SerializeField] private GameObject blackboardContent;
    [SerializeField] private GameObject blackboardButtons;
    [SerializeField] private GameObject badgesHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(displayBlackboardContent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator displayBlackboardContent()
    {
        yield return new WaitForSeconds(2.0f);
        logoAnimator.SetBool("logoFadeOut", true);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("logo").SetActive(false);
        blackboardContent.SetActive(true);
        blackboardAnimator.SetBool("contentFadeIn", true);
        yield return new WaitForSeconds(1.0f);
        blackboardButtons.SetActive(true);
        badgesHandler.SetActive(true);
    }
}
