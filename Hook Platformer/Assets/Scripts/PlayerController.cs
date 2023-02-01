using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public pausemenu script;
    public GameObject game0ver;
    
    public Vector3 velocity;
    private enum State { ON_GROUND, JUMPING, FALL_UP, FALLING, CROUCHED, FAILED}
    private State playerstate;
    private bool jumpInput;
    private bool is_on_ground;


    public float speed = 70.0f;
    public float jump_velocity = 10f;

    //gravity things
    public float gravity = 130.0f;
    public float jump_gravity = 50f;
    public float up_gravity = 80f;

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
    public TMP_Text healthText;
    public healthBar healthBar;
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
        healthBar.SetMaxHealth(heartCount + 1);
        respawnPoints = GetRespawnPoints();

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

            case State.FAILED:
                
                
                velocity.x = 0;
                //velocity.y = 0;
                //print("You Failed");
                
                    
                break;
        }


        if (!is_on_ground){
            velocity.y -= gravity * Time.deltaTime;
            
        }

        velocity = velocity + Grapple();
        controller.Move((velocity) * Time.deltaTime);

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        

        RaycastHit hitRight;
        
        if (Physics.Raycast(transform.position, Vector3.right, out hitRight, raycastRightDistrance, layerMask))
        {
            
            //RaycastHit hitRight = Physics.Raycast(transform.position, Vector3.right, raycastRightDistrance, layerMask);
            if (hitRight.collider.gameObject.layer == 7)
            {
                
                velocity.x = bounceBack;
                hitRight.collider.gameObject.layer = 8;
                


                
                heartCount--;
                
                healthText.GetComponent<TMP_Text>().text = "Health: " + heartCount.ToString();
                healthBar.SetHealth(heartCount + 1);
                if (heartCount == 0)
                {
                    playerstate = State.FAILED;
                    GameOver();
                }
                StartCoroutine(stun(1));
                /*playerstate = State.FAILED;
                while (getUpTime > 0)
                {
                    getUpTime -= Time.deltaTime;
                }
                if (getUpTime <= 0)
                {
                    playerstate = State.ON_GROUND;

                    getUpTime = 5.0f;
                }*/
            }
            else if (hitRight.collider.gameObject.layer == 3)
            {
                playerstate = State.FAILED;
                GameOver();

            }
            else if (hitRight.collider.gameObject.layer == 9)
            {
                playerstate = State.FAILED;
                FinishLevel(currentLevel);

            }
            else if (hitRight.collider.gameObject.layer == 10)
            {
                gameObject.transform.position = respawnLocation;
                




                heartCount--;

                healthText.GetComponent<TMP_Text>().text = "Health: " + heartCount.ToString();
                healthBar.SetHealth(heartCount + 1);
                if (heartCount == 0)
                {
                    playerstate = State.FAILED;
                    GameOver();
                }
                StartCoroutine(stun(0));



            }
        }
        
        if (Physics.Raycast(transform.position + shiftUD, Vector3.right, out hitRight, raycastRightDistrance, layerMask))
        {
            //RaycastHit2D hitRight = Physics2D.Raycast(transform.position + shiftUD, Vector2.right, raycastRightDistrance, layerMask);
            if (hitRight.collider.gameObject.layer == 7)
            {
                velocity.x = bounceBack;
                hitRight.collider.gameObject.layer = 8;

                
                heartCount--;

                healthText.GetComponent<TMP_Text>().text = "Health: " + heartCount.ToString();
                healthBar.SetHealth(heartCount + 1);
                if (heartCount == 0)
                {
                    playerstate = State.FAILED;
                    GameOver();
                }
                StartCoroutine(stun(1));
                /*playerstate = State.FAILED;
                while (getUpTime > 0)
                {
                    getUpTime -= Time.deltaTime;
                }
                if (getUpTime <= 0)
                {
                    playerstate = State.ON_GROUND;

                    getUpTime = 5.0f;
                }*/
            }
            else if (hitRight.collider.gameObject.layer == 3)
            {
                playerstate = State.FAILED;
                GameOver();
            }
            else if (hitRight.collider.gameObject.layer == 9)
            {
                playerstate = State.FAILED;
                FinishLevel(currentLevel);

            }
            else if (hitRight.collider.gameObject.layer == 10)
            {
                gameObject.transform.position = respawnLocation;





                heartCount--;

                healthText.GetComponent<TMP_Text>().text = "Health: " + heartCount.ToString();
                healthBar.SetHealth(heartCount + 1);
                if (heartCount == 0)
                {
                    playerstate = State.FAILED;
                    GameOver();
                }
                StartCoroutine(stun(0));


            }
        }

        if (Physics.Raycast(transform.position - shiftUD, Vector3.right, out hitRight, raycastRightDistrance, layerMask))
        {
            //RaycastHit2D hitRight = Physics2D.Raycast(transform.position - shiftUD, Vector2.right, raycastRightDistrance, layerMask);
            if (hitRight.collider.gameObject.layer == 7)
            {
                velocity.x = bounceBack;
                hitRight.collider.gameObject.layer = 8;

                
                heartCount--;

                healthText.GetComponent<TMP_Text>().text = "Health: " + heartCount.ToString();
                healthBar.SetHealth(heartCount + 1);
                if (heartCount == 0)
                {
                    playerstate = State.FAILED;
                    GameOver();
                }
                StartCoroutine(stun(1));
                
                /*playerstate = State.FAILED;
                while (getUpTime > 0)
                {
                    getUpTime -= Time.deltaTime;
                }
                if (getUpTime <= 0)
                {
                    playerstate = State.ON_GROUND;

                    getUpTime = 5.0f;
                }*/



            }
            else if (hitRight.collider.gameObject.layer == 3)
            {
                playerstate = State.FAILED;
                GameOver();
            }
            else if (hitRight.collider.gameObject.layer == 9)
            {
                playerstate = State.FAILED;

                FinishLevel(currentLevel);

            }
            else if (hitRight.collider.gameObject.layer == 10)
            {
                gameObject.transform.position = respawnLocation;





                heartCount--;

                healthText.GetComponent<TMP_Text>().text = "Health: " + heartCount.ToString();
                healthBar.SetHealth(heartCount + 1);
                if (heartCount == 0)
                {
                    playerstate = State.FAILED;
                    GameOver();
                }
                StartCoroutine(stun(0));


            }

        }


        if (Physics.Raycast(transform.position, Vector3.down, out hitRight, raycastDownDistrance, layerMask))
        {
            //RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, raycastDownDistrance, layerMask);
            if (hitRight.collider.gameObject.layer == 7)
            {
                velocity.y = jump_velocity;
                playerstate = State.FALL_UP;
                hitRight.collider.gameObject.layer = 8;
            }
            else if (hitRight.collider.gameObject.layer == 10)
            {
                gameObject.transform.position = respawnLocation;





                heartCount--;

                healthText.GetComponent<TMP_Text>().text = "Health: " + heartCount.ToString();
                healthBar.SetHealth(heartCount + 1);
                if (heartCount == 0)
                {
                    playerstate = State.FAILED;
                    GameOver();
                }
                StartCoroutine(stun(0));


            }
        }
        /*if (Physics.SphereCast(transform.position, 4, Vector3.left, out hitRight, respawnLayerMask))
        {

            nearestRespawnPoint = hitRight.collider.gameObject;
            respawnLocation = new Vector3(nearestRespawnPoint.transform.position.x, nearestRespawnPoint.transform.position.y + 1, 0);

        }*/
        float cd1 = maxRespawnLength;
        Transform co1 = null;

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            Transform respawnPoint = respawnPoints[i];
            if (transform.position.x > respawnPoint.position.x) continue;
            float dist = Vector2.Distance(respawnPoint.position, this.transform.position);
            if (dist < cd1) { co1 = respawnPoint; cd1 = dist; }
        }



        if (co1 != null && cd1 <= maxGrappleLength)
        {
            currentRespawnTarget = co1;
             
            respawnLocation = new Vector3(currentRespawnTarget.transform.position.x, currentRespawnTarget.transform.position.y + 4, 0);
        }

        if (heartCount == 0)
        {
            playerstate = State.FAILED;
            GameOver();
        }
        

    }

    State Detect_State()
    {

        if (controller.isGrounded)
        {
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

    Vector3 Grapple()
    {
        
        if(Input.GetKeyDown(KeyCode.Q)){
            SoundEffects.Effect grappleConnect = SoundEffects.getInstance().grappleConnect;
            grappleConnect.source.PlayOneShot(grappleConnect.clip, grappleConnect.volume);
            float cd = maxGrappleLength;
            Transform co = null;

            for(int i = 0; i<grappleables.Length;i++){
                Transform grappleable = grappleables[i];
                if(transform.position.x > grappleable.position.x) continue;
                float dist = Vector2.Distance(grappleable.position, this.transform.position);
                if(dist < cd){ co = grappleable; cd = dist;} 
            }

     

            if(co != null && cd <= maxGrappleLength){
                currentGrappleTarget = co;
                grappleRope.Grapple(co);
            }
        }

        if(Input.GetKeyUp(KeyCode.Q)){
            currentGrappleTarget = null;
            SoundEffects.Effect grappleRelease = SoundEffects.getInstance().grappleRelease;
            grappleRelease.source.PlayOneShot(grappleRelease.clip, grappleRelease.volume); 
            grappleRope.UnGrapple();
            
        }

        if(currentGrappleTarget != null){
            float angle = Mathf.Atan2(currentGrappleTarget.position.y - this.transform.position.y, currentGrappleTarget.position.x - this.transform.position.x);
            if(angle>maxAngle){
                currentGrappleTarget = null;
                SoundEffects.Effect grappleRelease = SoundEffects.getInstance().grappleRelease;
                grappleRelease.source.PlayOneShot(grappleRelease.clip, grappleRelease.volume); 
                grappleRope.UnGrapple();
                }
            return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0) * grappleStrength * Time.deltaTime;
        }

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
        GameObject[] temp = GameObject.FindGameObjectsWithTag("grappleable");
        List<Transform> temp2 = new List<Transform>();
        foreach(GameObject go in temp)
           temp2.Add(go.transform);
        return temp2.ToArray();
    }
    Transform[] GetRespawnPoints()
    {
        GameObject[] temp3 = GameObject.FindGameObjectsWithTag("Respawn");
        List<Transform> temp4 = new List<Transform>();
        foreach (GameObject go in temp3)
            temp4.Add(go.transform);
        return temp4.ToArray();
    }
}