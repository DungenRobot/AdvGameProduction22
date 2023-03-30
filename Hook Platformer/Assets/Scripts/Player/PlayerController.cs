using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public pausemenu script;
    public GameObject game0ver;
    public GameObject Reload;
    public GameObject Menu;
    
    public Vector3 velocity;
    private enum State { ON_GROUND, JUMPING, FALL_UP, FALLING, CROUCHED, FAILED}
    private State playerstate;
    private bool jumpInput;
    private bool is_on_ground;
    private float last_jumped = 100f;
    private float last_grounded = 100f;

    public float speed = 70.0f;
    public float jump_velocity = 10f;

    //gravity things
    public float gravity = 130.0f;
    public float jump_gravity = 50f;
    public float up_gravity = 80f;

    //platforming stuff
    public float time_hold_jump = 0.2f;
    public float time_hold_grounded = 0.1f;

    //CollisionStuff
    public float raycastRightDistrance = 4.0f;
    public float raycastDownDistrance = 4.0f;
    public float bounceBack = -18.0f;
    private Vector3 shiftUD = new Vector3(0, 0.75f, 0);
    public float stunTime = 1.5f;
    private Vector3 respawnLocation = new Vector3(0, 0, 0);
    public Transform[] respawnPoints;
    private Transform currentRespawnTarget = null;
    private float maxRespawnLength = 10;
    public ScreenShake ScreenShake;
    public TMP_Text TutorialText;


    // Grapple Stuff
    public float grappleStrength = 1;
    public float maxGrappleLength = 10000;
    public Transform[] grappleables;
    private Transform currentGrappleTarget = null;
    public GrappleRope grappleRope;
    public float maxAngle = (3/4)*Mathf.PI;

    //Audio
    public AudioClip[] audioClip;
    public float soundToPlay = -1.0f;
    private AudioSource audioSource;

    //Health Stuff
    public int heartCount = 2; //Make it one less than desired value
    
    public HealthbarV2 HealthbarV2;
    private GameObject nearestRespawnPoint;

    //Endings
    public GameObject gameOverText;
    public GameObject winLevelText;

    //Datahandler
    public GameObject DataHandler;
    //Current level
    public int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        DataHandler = GameObject.Find("DataHandler");
        grappleables = GetGrappleables();
        audioSource = GetComponent<AudioSource>();
        respawnPoints = GetRespawnPoints();

        HealthbarV2.SetHealth(3);

    }

    // Update is called once per frame
    void Update()
    {
        jumpInput = Input.GetButton("Jump");
        if (jumpInput) {last_jumped = 0f;}

        is_on_ground = controller.isGrounded;
        if (is_on_ground) {last_grounded = 0f;}

        switch (playerstate)
        {
            //if the player is on the ground
            case State.ON_GROUND:

                velocity.x = Mathf.Lerp(velocity.x, speed, Time.deltaTime * 0.5f);
                
                velocity.y = -gravity * Time.deltaTime;

                if (last_jumped < time_hold_jump) {
                    velocity.y = jump_velocity;
                    playerstate = State.JUMPING;
                    
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
                if (last_grounded < time_hold_grounded && jumpInput) {
                    velocity.y = jump_velocity;
                    playerstate = State.JUMPING;
                }
                if (controller.isGrounded)
                {
                    playerstate = State.ON_GROUND;
                }
                else 
                {
                    velocity.y -= gravity * Time.deltaTime;
                }
                break;
               

            case State.FAILED:
                
                
                velocity.x = 0;
                if (is_on_ground){
                    velocity.y = -gravity;
                    break;
                }
                velocity.y -= gravity;
    
                break;
        }

        last_jumped += Time.deltaTime;

        velocity = velocity + Grapple(); // Add ze grapple force to ze velocity
        controller.Move((velocity) * Time.deltaTime); // MOOOOVE

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        

        RaycastHit hitRight;
        
        if (Physics.Raycast(transform.position, Vector3.right, out hitRight, raycastRightDistrance, layerMask))
        {
            HitOutputs(hitRight.collider.gameObject.layer, hitRight.collider.gameObject);
            //RaycastHit hitRight = Physics.Raycast(transform.position, Vector3.right, raycastRightDistrance, layerMask);
            //if (hitRight.collider.gameObject.layer == 7)
            //{
            //    HitOutputs(7, hitRight.collider.gameObject);

            //}
            //else if (hitRight.collider.gameObject.layer == 3)
            //{
            //    HitOutputs(3, hitRight.collider.gameObject);

            //}
            //else if (hitRight.collider.gameObject.layer == 9)
            //{
            //    HitOutputs(9, hitRight.collider.gameObject);

            //}
            //else if (hitRight.collider.gameObject.layer == 10)
            //{
            //    HitOutputs(10, hitRight.collider.gameObject);
            //}
        }
        
        if (Physics.Raycast(transform.position + shiftUD, Vector3.right, out hitRight, raycastRightDistrance, layerMask))
        {
            HitOutputs(hitRight.collider.gameObject.layer, hitRight.collider.gameObject);
            
        }

        if (Physics.Raycast(transform.position - shiftUD, Vector3.right, out hitRight, raycastRightDistrance, layerMask))
        {
            HitOutputs(hitRight.collider.gameObject.layer, hitRight.collider.gameObject);
            

        }


        if (Physics.Raycast(transform.position, Vector3.down, out hitRight, raycastDownDistrance, layerMask))
        {
            //RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, raycastDownDistrance, layerMask);
            /*if (hitRight.collider.gameObject.layer == 7)
            {
                HitOutputs(7, hitRight.collider.gameObject);
                velocity.y = jump_velocity;
                playerstate = State.FALL_UP;
                hitRight.collider.gameObject.layer = 8;
            }*/
            if (hitRight.collider.gameObject.layer == 10)
            {
                HitOutputs(10, hitRight.collider.gameObject);

            }
        }
        if (heartCount == 0)
        {
            playerstate = State.FAILED;
            GameOver();
            return;
        }

        float cd1 = maxRespawnLength;
        Transform co1 = null;

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            Transform respawnPoint = respawnPoints[i];
            if (transform.position.x > respawnPoint.position.x) continue;
            float dist = Vector2.Distance(respawnPoint.position, this.transform.position);
            if (dist < cd1) { co1 = respawnPoint; cd1 = dist; }
        }



        if (co1 != null && cd1 <= maxRespawnLength)
        {
            currentRespawnTarget = co1;
             
            respawnLocation = new Vector3(currentRespawnTarget.transform.position.x, currentRespawnTarget.transform.position.y + 4, 0);
        }

        

        HealthbarV2.SetHealth(heartCount);
        
        //test for screen shake
        if(Input.GetKeyDown(KeyCode.O))
        {
            ScreenShake.GetComponent<ScreenShake>().Shaking();
        }
        

    }

    State Detect_State()
    {

        if (controller.isGrounded)
        {
            //if player just landed on ground
            if(playerstate == State.FALLING){
                SoundEffects.Effect playerLand = SoundEffects.getInstance().playerLand;
                playerLand.source.PlayOneShot(playerLand.clip, playerLand.volume);
            }
            return (State.ON_GROUND);
        }

        if (velocity.y > 0)
        {
            if (jumpInput)
            {
                //if player just jumped
                if(playerstate != State.JUMPING){ 
                    SoundEffects.Effect playerJump = SoundEffects.getInstance().playerJump;
                    playerJump.source.PlayOneShot(playerJump.clip, playerJump.volume);
                }
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


    private float timeGrappled = 0; // Shhh this isn't here
    Vector3 Grapple()
    {
        // This Does Grappling!
        
        if(Input.GetKeyDown(KeyCode.Q)){ // The player pressed q (this means they want to grapple hooray)
            // Play SFX
            SoundEffects.Effect grappleConnect = SoundEffects.getInstance().grappleConnect;
            grappleConnect.source.PlayOneShot(grappleConnect.clip, grappleConnect.volume);

            // Get The Closest Grapple
            float currentDistance = maxGrappleLength;
            Transform currentObject = null;

            for(int i = 0; i<grappleables.Length;i++){
                Transform grappleable = grappleables[i];
                if(transform.position.x > grappleable.position.x) continue;
                float dist = Vector2.Distance(grappleable.position, this.transform.position);
                if(dist < currentDistance){ currentObject = grappleable; currentDistance = dist;} 
            }

     
            // If There Is A Closest Object, And Its Closer Than Max Distance, Connect Grapple
            if(currentObject != null && currentDistance <= maxGrappleLength){
                currentGrappleTarget = currentObject;
                timeGrappled = 0;
                grappleRope.Grapple(currentObject); // make rope appear
            }
        }

        // Player let go of q, this probably means they are done grappling
        if(Input.GetKeyUp(KeyCode.Q)){
            currentGrappleTarget = null; // Get Rid Of Grapple Target
            // Play SFX
            SoundEffects.Effect grappleRelease = SoundEffects.getInstance().grappleRelease;
            grappleRelease.source.PlayOneShot(grappleRelease.clip, grappleRelease.volume);
            // Get rid of rope 
            grappleRope.UnGrapple();
            
        }


        // If We are currently grappling to something
        if(currentGrappleTarget != null){
            // get the angle to that thing
            float angle = Mathf.Atan2(currentGrappleTarget.position.y - this.transform.position.y, currentGrappleTarget.position.x - this.transform.position.x);
            if(angle>maxAngle){ // if the angle is too high, disconnect
                currentGrappleTarget = null; 
                // play sfx
                SoundEffects.Effect grappleRelease = SoundEffects.getInstance().grappleRelease;
                grappleRelease.source.PlayOneShot(grappleRelease.clip, grappleRelease.volume); 
                grappleRope.UnGrapple(); // make rope go away
                }
               timeGrappled += Time.deltaTime; // Increase the amount of time grappled 
                // return the force that should be added

                float lerpVal = (Mathf.Exp(timeGrappled * 2) - 1) / (Mathf.Exp(timeGrappled * 2) + 1);

            return Vector3.Lerp(Vector3.zero, new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0) * grappleStrength * Time.deltaTime, lerpVal);
        }  
        // return no force because there shouldn't be grapple force if the player isn't grappling
        return new Vector3(0,0,0);
    }

    void GameOver()
    {

        print("You Failed");
        gameOverText.SetActive(true);
        script.g0 = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        //DO GAMEOVER
    }

    void FinishLevel(int thisLevel)
    {

        DataHandler.GetComponent<DataHandler>().LevelClear(thisLevel);
        
        winLevelText.SetActive(true);
        script.g0 = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    IEnumerator stun(int objectHit)
    {
        if (objectHit == 1)
        {
            yield return new WaitForSeconds(1);
        }

        playerstate = State.FAILED;

        yield return new WaitForSeconds(stunTime);

        playerstate = State.ON_GROUND;

        

    }



    Transform[] GetGrappleables(){
        GameObject[] grapplePoints = GameObject.FindGameObjectsWithTag("grappleable"); // Get All The Grapple Points
        List<Transform> grapplePointsList = new List<Transform>(); // Have a list to add the transforms of all the objects because I don't want to keep refrences to the objects
        foreach(GameObject go in grapplePoints) // loop through points
           grapplePointsList.Add(go.transform); // add the transform of the point to the list
        return grapplePointsList.ToArray(); // return the list as an array
    }
    Transform[] GetRespawnPoints()
    {
        GameObject[] temp3 = GameObject.FindGameObjectsWithTag("Respawn");
        List<Transform> temp4 = new List<Transform>();
        foreach (GameObject go in temp3)
            temp4.Add(go.transform);
        return temp4.ToArray();
    }

    void HitOutputs(int objLayer, GameObject obstacle)
    {
        if (objLayer == 7)
        {
            //calls screen shake 
            StartCoroutine(ScreenShake.GetComponent<ScreenShake>().Shaking());
            
            velocity.x = bounceBack;
            obstacle.layer = 8;

            heartCount--;

            if (heartCount == 0)
            {
                playerstate = State.FAILED;
                GameOver();
            }
            StartCoroutine(stun(1));
            
        }
        else if (objLayer == 3)
        {
            playerstate = State.FAILED;
            GameOver();

        }
        else if (objLayer == 9)
        {
            playerstate = State.FAILED;
            FinishLevel(currentLevel);

        }
        else if (objLayer == 10)
        {
            gameObject.transform.position = respawnLocation;





            heartCount--;



            if (heartCount == 0)
            {
                playerstate = State.FAILED;
                GameOver();
            }
            StartCoroutine(stun(0));



        }
        else if (objLayer == 12)
        {

            StartCoroutine(tutPause(TutorialText));
            obstacle.layer = 8;

        }
        else if (objLayer == 13)
        {
           
            TutorialText.GetComponent<TMP_Text>().text = "Press [Q] to Grapple";
            StartCoroutine(tutPause(TutorialText));
            obstacle.layer = 8;

        }
        else if (objLayer == 14)
        {
            velocity.x = velocity.x + 3;
            
        }
    }

    IEnumerator tutPause(TMP_Text tutText)
    {
        //On the top of Yield Return,
        //It stops time
        Time.timeScale = 0;
        Menu.gameObject.SetActive(false);
        Reload.gameObject.SetActive(false);
        tutText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        tutText.gameObject.SetActive(false);
        Time.timeScale = 1;
        Menu.gameObject.SetActive(true); 
        Reload.gameObject.SetActive(true);
        
    }
}