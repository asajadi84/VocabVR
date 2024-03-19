using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneBadgeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] badges;
    [SerializeField] private Sprite maxScoreBadgeSprite;

    private float[] sessionScores = new float[6];
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sessionScores.Length; i++)
        {
            string key = "scoreSession" + (i + 1);
            sessionScores[i] = PlayerPrefs.GetFloat(key, 0);

            if (sessionScores[i] != 0)
            {
                badges[i].SetActive(true);
                badges[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = sessionScores[i].ToString();
                
                //max score
                if (sessionScores[i]> 19.5f)
                {
                    badges[i].GetComponent<Image>().sprite = maxScoreBadgeSprite;
                    badges[i].transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.black;
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
