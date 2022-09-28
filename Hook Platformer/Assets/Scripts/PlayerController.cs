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




    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = speed;

        is_on_ground = controller.isGrounded;

        if (is_on_ground && Input.GetButton("Jump")){
            velocity.y = 5f;
        }

        if (!is_on_ground){
            velocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
        print(velocity * Time.deltaTime);
    }

}
