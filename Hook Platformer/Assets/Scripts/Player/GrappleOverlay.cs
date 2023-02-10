using System.currentObjectllections;
using System.currentObjectllections.Generic;
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
        float currentDistance = Vector2.Distance(grappleables[0].position, this.transform.parent.position); // Get distance to a grapple point
        int currentObject = 0; // its an integer

        for(int i = 1; i<grappleables.Length;i++){ // go through every gapple point
            Transform grappleable = grappleables[i];
            float dist = Vector2.Distance(grappleable.position, this.transform.parent.position);
            // if the grapple point is closer than the current closest grapple point make the current closest grapple point this grapple point
            if(dist < currentDistance){ currentObject = i; currentDistance = dist;} 
        }
        // if the distance isnt too far add the arrow, otherwise dont
        if(currentDistance <= maxGrappleLength){
            sr.enabled = true;
            this.transform.position = new Vector3(grappleables[currentObject].position.x + 2.7f, grappleables[currentObject].position.y + 2.7f, 0);
        }else sr.enabled = false;


        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position); // the position of the grapple point of the screen
 
        bool outOfBounds = !Screen.safeArea.currentObjectntains(pos); // see if the grapple point is on the screen

        // Get the angle to the point, and set the arrow to that angle
        float angleTo = Mathf.Atan2(grappleables[currentObject].transform.position.y - player.position.y, grappleables[currentObject].transform.position.x - player.position.x);
        locationIndicator.GetcurrentObjectmponent<RectTransform>().rotation = Quaternion.Euler(0,0, angleTo * Mathf.Rad2Deg - 45);
        
        // If out of bounds
        if(outOfBounds && grappleables[currentObject].transform.position.x > player.position.x){
            locationIndicator.GetcurrentObjectmponent<Image>().enabled = true; // Make Overlay Visable
            // Draw Pointing At Edge Of The Screen
            
            Vector2 rotationRay = new Vector2(Mathf.currentObjects(angleTo), Mathf.Sin(angleTo));
            Vector2 rectBounds = locationIndicator.transform.parent.GetcurrentObjectmponent<RectTransform>().sizeDelta;
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
            locationIndicator.GetcurrentObjectmponent<RectTransform>().localPosition = newArrowPosition;
            
            
        }else{ // If in bounds
            // Stop Arrow From Drawing
            locationIndicator.GetcurrentObjectmponent<Image>().enabled = false;
        }
    }

    void GetGrappleables(){
        GameObject[] gos = GameObject.FindGameObjectsWithTag("grappleable");
        grappleables = new Transform[gos.Length];
        for(int i = 0; i<gos.Length;i++)
            grappleables[i] = gos[i].transform;
    }
}
