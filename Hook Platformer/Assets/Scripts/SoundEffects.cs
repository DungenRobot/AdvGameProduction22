using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource source;

    // Jumping
    public Effect playerLand;
    public void onPlayerLand(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    public Effect playerJump;
    public void onPlayerJump(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    public Effect playerInAir;
    public void onPlayerInAir(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    // Grapple
    public Effect grappleConnect;
    public void onPlayerLand(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    public Effect grappleMove;
    public void onPlayerLand(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    public Effect grappleRelease;
    public void onPlayerLand(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    // Skateboard
    public Effect skateboardCrash;
    public void onPlayerLand(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    public Effect skateboardRoll;
    public void onPlayerLand(){ source.PlayOneShot(playerLand.clip, playerLand.volume); }

    private static SoundEffects inst;
    public static getInstance(){
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
        AudioClip clip;
        float volume;
    }
    
}
