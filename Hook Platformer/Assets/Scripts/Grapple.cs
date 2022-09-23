using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Rigidbody2D rb;
    public float grappleStrength = 1;
    public float maxGrappleLength = 3;
    private Transform[] grappleables;
    private Transform currentTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        GetGrappleables();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            float cd = Vector2.Distance(grappleables[0].position, this.transform.position);
            Transform co = grappleables[0];

            for(int i = 1; i<grappleables.Length;i++){
                Transform grappleable = grappleables[i];
                float dist = Vector2.Distance(grappleable.position, this.transform.position);
                if(dist < cd) co = grappleable;
            }

            if(cd <= maxGrappleLength) currentTarget = co;
        }

        if(Input.GetKeyUp(KeyCode.Q)) currentTarget = null;

        if(currentTarget != null){
            float angle = Mathf.Atan2(currentTarget.position.y - this.transform.position.y, currentTarget.position.x - this.transform.position.x);
           // Kinemetic rb.velocity = rb.velocity + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * grappleStrength
           // Dyamic rb.AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * grappleStrength);
        }
    }

    Transform[] GetGrappleables(){
        GameObject[] temp = GameObject.FindGameObjectsWithTag("grappleable");
        List<Transform> temp2 = new List<Transform>();
        foreach(GameObject go in temp){
            temp2.Add(go.transform);
        }
        grappleables = temp2.ToArray();
        return grappleables;
    }
}
