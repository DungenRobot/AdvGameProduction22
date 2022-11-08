using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 velocity;

    private enum State { ON_GROUND, JUMPING, FALL_UP, FALLING}
    private State playerstate;

    private bool jumpInput = false;
    private bool is_on_ground = false;

    public float speed = 70.0f;
    public float jump_velocity = 10f;

    //gravity things
    public float gravity = 130.0f;
    public float jump_gravity = 50f;
    public float up_gravity = 80f;

    //CollisionStuff
    public float raycastRightDistrance = 4.0f;
    public float raycastDownDistrance = 4.0f;
    public float slowOnHit = 0.33f;
    private Vector3 shiftUD = new Vector3(0, 0.75f, 0);

    // Grapple Stuff
    public float grappleStrength = 1;
    public float maxGrappleLength = 10000;
    public Transform[] grappleables;
    private Transform currentGrappleTarget = null;
    public GrappleRope grappleRope;

    //Audio
    public AudioClip jumpAudio;
    public AudioClip crashAudio;
    private AudioSource audioSource;

    //Tricks


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        grappleables = GetGrappleables();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpInput = Input.GetButton("Jump");

        is_on_ground = controller.isGrounded;

        //Debug.Log(playerstate);

        switch (playerstate)
        {
            case State.ON_GROUND:
                velocity.x = Mathf.Lerp(velocity.x, speed, Time.deltaTime * 0.5f);
                if (jumpInput) {
                    velocity.y = jump_velocity;
                    playerstate = State.JUMPING;
                    audioSource.clip = jumpAudio;
                    audioSource.Play();
                }
                else if (!controller.isGrounded) {
                    playerstate = State.FALLING;
                }
                break;

            case State.JUMPING:
                velocity.y -= jump_gravity * Time.deltaTime;
                playerstate = Detect_State();
                break;

            case State.FALL_UP:
                velocity.y -= up_gravity * Time.deltaTime ;
                playerstate = Detect_State();
                break;

            case State.FALLING:
                if (controller.isGrounded)
                {
                    playerstate = State.ON_GROUND;
                }
                break;
        }

        if (!is_on_ground){
            velocity.y -= gravity * Time.deltaTime;
            
        }

        velocity = velocity + Grapple();
        controller.Move((velocity) * Time.deltaTime);

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        

        if (Physics2D.Raycast(transform.position, Vector2.right, raycastRightDistrance, layerMask))
        {
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, raycastRightDistrance, layerMask);
            if (hitRight.collider.gameObject.layer == 7)
            {
                velocity.x = velocity.x * slowOnHit;
                hitRight.collider.gameObject.layer = 8;
                
                audioSource.clip = crashAudio;
                audioSource.Play();
            }
        }

        if (Physics2D.Raycast(transform.position + Vector3.up, Vector2.right, raycastRightDistrance, layerMask))
        {
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + shiftUD, Vector2.right, raycastRightDistrance, layerMask);
            if (hitRight.collider.gameObject.layer == 7)
            {
                velocity.x = velocity.x * slowOnHit;
                hitRight.collider.gameObject.layer = 8;

                audioSource.clip = crashAudio;
                audioSource.Play();
            }
        }

        if (Physics2D.Raycast(transform.position + Vector3.down, Vector2.right, raycastRightDistrance, layerMask))
        {
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position - shiftUD, Vector2.right, raycastRightDistrance, layerMask);
            if (hitRight.collider.gameObject.layer == 7)
            {
                velocity.x = velocity.x * slowOnHit;
                hitRight.collider.gameObject.layer = 8;

                audioSource.clip = crashAudio;
                audioSource.Play();
            }
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDownDistrance, layerMask))
        {
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, raycastDownDistrance, layerMask);
            if (hitDown.collider.gameObject.layer == 7)
            {
                velocity.y = jump_velocity;
                playerstate = State.FALL_UP;
                hitDown.collider.gameObject.layer = 8;
            }
        }

        
    }

    State Detect_State()
    {

        if (controller.isGrounded)
        {
            return (State.ON_GROUND);
        }

        if (velocity.y > 0)
        {
            if (jumpInput)
            {
                return (State.JUMPING);
            }
            else
            {
                return (State.FALL_UP);
            }
            
        }
        else
        {
            return (State.FALLING);
        }
    }

    Vector3 Grapple()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            float cd = Vector2.Distance(grappleables[0].position, this.transform.position);
            Transform co = grappleables[0];

            for(int i = 1; i<grappleables.Length;i++){
                Transform grappleable = grappleables[i];
                float dist = Vector2.Distance(grappleable.position, this.transform.position);
                if(dist < cd){ co = grappleable; cd = dist;} 
            }

            if(cd <= maxGrappleLength){
                currentGrappleTarget = co;
                grappleRope.Grapple(co);
            }
        }

        if(Input.GetKeyUp(KeyCode.Q)){
            currentGrappleTarget = null; 
            grappleRope.UnGrapple();
        }

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