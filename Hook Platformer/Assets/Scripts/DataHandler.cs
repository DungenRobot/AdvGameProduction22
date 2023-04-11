using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;

    public int LatestLevel;
    public bool ls;


    void Awake()
    {
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    public void LevelClear(int level)
    {
        if (level > LatestLevel){
            LatestLevel = level;
        }
    }
}
