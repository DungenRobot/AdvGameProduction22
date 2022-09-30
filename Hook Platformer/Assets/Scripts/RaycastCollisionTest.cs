using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisionTest : MonoBehaviour
{
    public float raycastDistrance = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //LayerMask mask = LayerMask.GetMask("Wall");

        int layerMask = 1 << 6;
        layerMask = ~layerMask;
        /*
        
        
        if (Physics2D.Raycast(transform.position, Vector2.right, raycastDistrance, mask))
        {
            Destroy(gameObject);
        } */
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, raycastDistrance, layerMask);
        if (hit.collider.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
