using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanFallTrigger : MonoBehaviour
{
    private Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == 8)
        {
            animation.Play("Fall");
        }
    }
}
