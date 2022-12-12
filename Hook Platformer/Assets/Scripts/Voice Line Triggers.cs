using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTriggers : MonoBehaviour
{
    public AudioSource source;
    public VoiceLine[] fallLines;
    public VoiceLine[] failLines;
    public VoiceLine[] hitLines;
    public VoiceLine[] jumpLines;
    public VoiceLine[] grappleLines;
    public VoiceLine[] randomLines;
    
    private int lastIndex = -1;

    private static VoiceLineTriggers inst;

    public static VoiceLineTriggers getInstance(){
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

    void onHit(){

    }

    void onFall(){

    }

    void onJump(){

    }

    void onGrapple(){

    }

    void Random(){

    }

    void PlaySound(VoiceLine clip){
        source.PlayOneShot(clip.clip, clip.volume);
    }

    [System.Serializable]
    public struct VoiceLine{
        public AudioClip clip;
        public float volume;
    }
}
