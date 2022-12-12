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
        
        if(!intro.isPlaying && !main.isPlaying)
        {
            main.Play(0);
        }
    }
}
