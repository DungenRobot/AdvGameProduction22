using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarV2 : MonoBehaviour
{
    public Sprite HealthThree;
    public Sprite HealthTwo;
    public Sprite HealthOne;
    public Sprite DEAD;

    public Sprite[] HealthSprites;
    
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = HealthThree;
        
    }

    public void SetHealth(int healthValue)
    {
        //get respective sprite to health value
        Debug.Log(healthValue);
        gameObject.GetComponent<Image>().sprite = HealthSprites[healthValue];
        if (healthValue < 0)
        {
            gameObject.GetComponent<Image>().sprite = HealthSprites[0];
        }
    }
}

//     public void Health3()
//     {
//         gameObject.GetComponent<Image>().sprite = HealthThree;
//     }

//     public void Health2()
//     {
//         gameObject.GetComponent<Image>().sprite = HealthTwo;
//     }

//     public void Health1()
//     {
//         gameObject.GetComponent<Image>().sprite = HealthOne;
//     }
//     public void Dead()
//     {
//         gameObject.GetComponent<Image>().sprite = DEAD;
//     }
//