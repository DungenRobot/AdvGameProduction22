using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisionTest : MonoBehaviour
{
    public float raycastDistrance = 1.0f;
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

        int layerMask = 1 << 7;
        //layerMask = ~layerMask;
        //RaycastHit hit = Physics.Raycast(transform.position, Vector3.right, raycastDistrance, layerMask);

        //RaycastHit hit = Physics.Raycast(transform.position, Vector3.right, raycastDistrance, layerMask);


        /*
        if (Physics.Raycast(transform.position, Vector3.right, raycastDistrance, layerMask))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, raycastDistrance, layerMask);
            Destroy(hit.collider.gameObject);
        }
        */

        if (Physics2D.Raycast(transform.position, Vector2.right, raycastDistrance, layerMask))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, raycastDistrance, layerMask);
            Destroy(hit.collider.gameObject);
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDistrance, layerMask))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistrance, layerMask);
            Destroy(hit.collider.gameObject);
        }


        //Used to test distance of the rays
        Vector2 down = transform.TransformDirection(Vector2.down) * raycastDistrance;
        Debug.DrawRay(transform.position, down, Color.blue);

        /*
        if (hit.collider.gameObject.layer == 7)
        {
            //velocity.x = velocity.x * slowOnHit;
            Destroy(hit.collider.gameObject);
        }*/
    }
}
