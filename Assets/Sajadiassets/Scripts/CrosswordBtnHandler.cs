using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrosswordBtnHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        /*DOTween.Sequence()
            .Append(transform.DOScale(new Vector2(1.1f, 1.1f), 0.3f).SetDelay(0.3f))
            .Append(transform.DOScale(Vector2.one, 0.3f))
            .SetLoops(-1, LoopType.Restart);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadCrosswordScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
