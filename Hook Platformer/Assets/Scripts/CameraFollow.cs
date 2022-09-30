using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    [Header("The Lag Speed, 1 Instant, 0 Doesn't Move")]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = cam.position;
        camPos.x = Mathf.Lerp(cam.position.x,player.position.x, speed);
        cam.position = camPos;
    }
}
