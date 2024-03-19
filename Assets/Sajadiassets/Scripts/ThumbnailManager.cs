using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThumbnailManager : MonoBehaviour
{

    [SerializeField] private GameObject thumbnail1;
    [SerializeField] private GameObject thumbnail2;
    [SerializeField] private GameObject thumbnail3;
    [SerializeField] private GameObject thumbnail4;
    [SerializeField] private GameObject thumbnail5;
    [SerializeField] private GameObject thumbnail6;
    
    [SerializeField] private Sprite[] images;

    // Start is called before the first frame update
    void Awake()
    {
        thumbnail1.GetComponent<Image>().sprite = images[0];
        thumbnail2.GetComponent<Image>().sprite = images[1];
        thumbnail3.GetComponent<Image>().sprite = images[2];
        thumbnail4.GetComponent<Image>().sprite = images[3];
        thumbnail5.GetComponent<Image>().sprite = images[4];
        thumbnail6.GetComponent<Image>().sprite = images[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
