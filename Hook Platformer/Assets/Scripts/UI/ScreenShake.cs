using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{ 
    //how long shake is
    public float duration = 0.2f;
    public AnimationCurve curve;
    
    public IEnumerator Shaking()
    {
        //collects/stores starting pos
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            startPosition = transform.position;
            //calculates and adds animation curve to screen shake
            float strength = curve.Evaluate(elapsedTime/duration);
            //moves camera randomly in accordance to strength
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        //moves camera back into origenal position after shake
        transform.position = startPosition;
    }
}
