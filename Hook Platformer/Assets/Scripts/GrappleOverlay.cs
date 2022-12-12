using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleOverlay : MonoBehaviour
{
    public SpriteRenderer sr;
    Transform[] grappleables;
    public float maxGrappleLength;
    // Start is called before the first frame update
    void Start()
    {
        GetGrappleables();
    }

    // Update is called once per frame
    void Update()
    {
        float cd = Vector2.Distance(grappleables[0].position, this.transform.parent.position);
        int co = 0;

        for(int i = 1; i<grappleables.Length;i++){
            Transform grappleable = grappleables[i];
            float dist = Vector2.Distance(grappleable.position, this.transform.parent.position);
            if(dist < cd){ co = i; cd = dist;} 
        }

        if(cd <= maxGrappleLength){
            sr.enabled = true;
            Debug.Log(grappleables[co].position.x + ", " +  grappleables[co].position.y);
            this.transform.position = new Vector3(grappleables[co].position.x, grappleables[co].position.y, 0);
        }else sr.enabled = false;
    }

    void GetGrappleables(){
        GameObject[] gos = GameObject.FindGameObjectsWithTag("grappleable");
        grappleables = new Transform[gos.Length];
        for(int i = 0; i<gos.Length;i++)
            grappleables[i] = gos[i].transform;
    }
}
