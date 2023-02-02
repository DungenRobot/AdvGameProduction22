using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarV2 : MonoBehaviour
{
    public Sprite HealthThree;
    public Sprite HealthTwo;
    public Sprite HealthOne;
    
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = HealthThree;
        
    }
    
    public void Health3()
    {
        gameObject.GetComponent<Image>().sprite = HealthThree;
    }

    public void Health2()
    {
        gameObject.GetComponent<Image>().sprite = HealthTwo;
    }

    public void Health1()
    {
        gameObject.GetComponent<Image>().sprite = HealthOne;
    }
}