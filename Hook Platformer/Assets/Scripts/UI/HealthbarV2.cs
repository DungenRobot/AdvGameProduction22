using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarV2 : MonoBehaviour
{
    public Sprite[] HealthSprites;

    public void SetHealth(int healthValue)
    {
        //get respective sprite to health value
        gameObject.GetComponent<Image>().sprite = HealthSprites[healthValue];
        if (healthValue < 0)
        {
            gameObject.GetComponent<Image>().sprite = HealthSprites[0];
        }
    }
}