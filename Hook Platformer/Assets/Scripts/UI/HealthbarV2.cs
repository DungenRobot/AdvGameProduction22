using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarV2 : MonoBehaviour
{
    public Sprite HealthThree;
    public Sprite HealthTwo;
    public Sprite HealthOne;
    
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = HealthThree;
    }
    
    public void Health3()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = HealthThree;
    }

    public void Health2()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = HealthTwo;
    }

    public void Health1()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = HealthOne;
    }
}