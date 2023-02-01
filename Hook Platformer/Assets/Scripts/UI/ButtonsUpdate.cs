using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsUpdate : MonoBehaviour
{
    private int latestLevel;
    public int buttonLevel;
    public GameObject LevelButton;


    void Start()
    {
        latestLevel = GameObject.Find("DataHandler").GetComponent<DataHandler>().LatestLevel;   
    }

    void Update()
    {
        if(latestLevel >= buttonLevel)
        {
            LevelButton.SetActive(true);
        }

        else
        {
            LevelButton.SetActive(false);
        }
    }
}
