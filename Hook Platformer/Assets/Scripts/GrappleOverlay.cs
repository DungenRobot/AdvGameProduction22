using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GrappleOverlay : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer sr;
    public GameObject locationIndicator;

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
            this.transform.position = new Vector3(grappleables[co].position.x, grappleables[co].position.y, 0);
        }else sr.enabled = false;

        // Indicator

        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
 
        bool outOfBounds = !Screen.safeArea.Contains(pos);

        float angleTo = Mathf.Atan2(grappleables[co].transform.position.y - player.position.y, grappleables[co].transform.position.x - player.position.x);
        locationIndicator.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0, angleTo * Mathf.Rad2Deg - 45);
        

        if(outOfBounds){
            locationIndicator.GetComponent<Image>().enabled = true; // Make Overlay Visable
            // Draw Pointing At Edge Of The Screen
            
            Vector2 rotationRay = new Vector2(Mathf.Cos(angleTo), Mathf.Sin(angleTo));
            Vector2 rectBounds = locationIndicator.transform.parent.GetComponent<RectTransform>().sizeDelta;
            Vector2 newArrowPosition = new Vector2(0,0);
        

            if(Mathf.Abs(rectBounds.y/rotationRay.y) < Mathf.Abs(rectBounds.x/rotationRay.x)){
                // Should be aligned on top or bottom
                if(rotationRay.y > 0) newArrowPosition.y = rectBounds.y/2 - 50;
                else newArrowPosition.y = 0 + 50;
                newArrowPosition.x = rotationRay.x * rectBounds.y;
            }else{
                // Should be aligned on left or right
                if(rotationRay.x > 0) newArrowPosition.x = rectBounds.x/2 - 50;
                else newArrowPosition.x = -rectBounds.x/2 + 50;
                newArrowPosition.y = rotationRay.y * (rectBounds.x/(2));
                
            }
        locationIndicator.GetComponent<RectTransform>().localPosition = newArrowPosition;
            
            
        }else{
            // Stop Arrow From Drawing
            locationIndicator.GetComponent<Image>().enabled = false;
        }
    }

    void Indicator(){

    }

    void GetGrappleables(){
        GameObject[] gos = GameObject.FindGameObjectsWithTag("grappleable");
        grappleables = new Transform[gos.Length];
        for(int i = 0; i<gos.Length;i++)
            grappleables[i] = gos[i].transform;
    }
}