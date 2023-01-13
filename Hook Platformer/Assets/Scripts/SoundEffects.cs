using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    // Jumping
    public Effect playerLand;
    public Effect playerJump;
    public Effect playerInAir;
    // Grapple
    public Effect grappleConnect;
    public Effect grappleMove;
    public Effect grappleRelease;

    // Skateboard
    public Effect skateboardCrash;
    public Effect skateboardRoll;

    private static SoundEffects inst;
    public static SoundEffects getInstance(){
        return inst;
    }

    // Start is called before the first frame update
    void Start()
    {
        inst = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class Effect{
        public AudioClip clip;
        public AudioSource source;
        public float volume;
    }
    
}
