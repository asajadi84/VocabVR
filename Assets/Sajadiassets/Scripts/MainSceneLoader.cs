using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{

    [SerializeField] private int sceneId;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneId);
    }
}
