using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool is_on_ground;

    public float speed = 70.0f;
    public float gravity = 130.0f;
    public float jump_velocity = 30f;


    // Grapple Stuff
    public float grappleStrength = 1;
    public float maxGrappleLength = 10000;
    public Transform[] grappleables;
    private Transform currentGrappleTarget = null;
    


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        grappleables = GetGrappleables();
    }

    // Update is called once per frame
    void Update()
    {
        

        is_on_ground = controller.isGrounded;

        if(is_on_ground) velocity.x = Mathf.Lerp(velocity.x, speed, Time.deltaTime * 0.5f);

        if (is_on_ground && Input.GetButton("Jump")){
            velocity.y = 5f;
        }

        if (!is_on_ground){
            velocity.y -= gravity * Time.deltaTime;
        }

        velocity = velocity + Grapple();
        controller.Move((velocity) * Time.deltaTime);
    }

    Vector3 Grapple()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("Q Down");
            float cd = Vector2.Distance(grappleables[0].position, this.transform.position);
            Transform co = grappleables[0];

            for(int i = 1; i<grappleables.Length;i++){
                Transform grappleable = grappleables[i];
                float dist = Vector2.Distance(grappleable.position, this.transform.position);
                if(dist < cd){ co = grappleable; cd = dist;} 
            }

            if(cd <= maxGrappleLength) currentGrappleTarget = co;
        }

        if(Input.GetKeyUp(KeyCode.Q)) currentGrappleTarget = null;

        if(currentGrappleTarget != null){

            float angle = Mathf.Atan2(currentGrappleTarget.position.y - this.transform.position.y, currentGrappleTarget.position.x - this.transform.position.x);
            return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0) * grappleStrength;
        }

        return new Vector3(0,0,0);
    }

    Transform[] GetGrappleables(){
        GameObject[] temp = GameObject.FindGameObjectsWithTag("grappleable");
        List<Transform> temp2 = new List<Transform>();
        foreach(GameObject go in temp)
           temp2.Add(go.transform);
        return temp2.ToArray();
    }
}