using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{

    public Line[] englishLines = {};
    public Line[] spanishLines = {};

    public float voiceLineDelay = 5; // Time between voice lines
    public float voiceLineVariationTime = 1; // Variation in time between voice liens
    private float voiceLineTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        voiceLineTimer -= Time.deltaTime;
        if(voiceLineTimer <= 0){
            voiceLineTimer = voiceLineDelay + Random.Range(-voiceLineVariationTime, voiceLineVariationTime);
            int voiceLine;
            switch(LanguageManager.getInstance().language){
                case "EN":
                voiceLine = Random.Range(0,englishLines.Length-1);
                englishLines[voiceLine].source.PlayOneShot(englishLines[voiceLine].clip, englishLines[voiceLine].volume);
                break;
                case "ES":
                voiceLine = Random.Range(0,spanishLines.Length-1);
                spanishLines[voiceLine].source.PlayOneShot(spanishLines[voiceLine].clip, spanishLines[voiceLine].volume);
                break;
            }
            
        }
    }


    [System.Serializable]
    public class Line{
        public AudioClip clip;
        public AudioSource source;
        public float volume;
    }
}
