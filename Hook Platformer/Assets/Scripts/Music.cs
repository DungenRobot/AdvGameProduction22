using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource intro;
    public AudioSource main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("MU");
        if(!intro.isPlaying && !main.isPlaying)
        {
            Debug.Log("MP");
            main.Play(0);
        }
    }
}
