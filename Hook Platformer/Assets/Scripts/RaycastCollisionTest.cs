using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisionTest : MonoBehaviour
{
    public float raycastDistrance = 4.0f;
    public float slowOnHit = 0.33f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //LayerMask mask = LayerMask.GetMask("Wall");

        
        /*
        
        
        if (Physics2D.Raycast(transform.position, Vector2.right, raycastDistrance, mask))
        {
            Destroy(gameObject);
        } */

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

       //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, raycastDistrance, layerMask);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5, Vector2.right, layerMask);
        
        
        if (hit.collider.gameObject.layer == 7)
        {
            //velocity.x = velocity.x * slowOnHit;
            Destroy(hit.collider.gameObject);
        }
    }
}
