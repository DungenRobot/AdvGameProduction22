using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public float passed = 0f;
    public TMP_Text clockText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        passed = passed + Time.deltaTime;
        clockText.SetText(""+passed);
    }
}
