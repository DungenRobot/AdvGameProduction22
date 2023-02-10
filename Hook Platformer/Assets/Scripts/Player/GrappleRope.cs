using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// IGNORE THIS ENTIRE SCRIPT IT JUST TAKES TWO POINTS AND DRAWS A LINE WITH SOME COOL EFFECTS I DONT KNOW WHAT IS GOING ON DONT EDIT IT
public class GrappleRope : MonoBehaviour{
  public Transform player;
  private Transform gp;
  public LineRenderer lr;
  public int percision = 40;
  [Range(0,20)] public float straightenLineSpeed = 5;
  public AnimationCurve ropeAnimationCurve;
  public AnimationCurve ropeProgressionCurve;
  public float ropeProgressionSpeed = 1;
  public float StartWaveSize = 2;
  private float waveSize;
  private float moveTime;
  
  //bool straightLine = true;
  //[HideInInspector] 
  private bool isGrappling = false;
  

  public void Start(){
    lr.enabled = false;
  }

  public void Grapple(Transform to){
    gp = to;
    moveTime = 0;
    lr.positionCount = percision;
    waveSize = StartWaveSize;
    //straightLine = false;
    LinePointsToFirePoint();
    lr.enabled = true;
    isGrappling = true;
  }

  public void UnGrapple(){
    lr.enabled = false;
    isGrappling = false;
  }

  private void LinePointsToFirePoint(){
    for(int i = 0; i<percision;i++){
        lr.SetPosition(i, gp.position);
    }
  }

public void DrawRope(){
  //Debug.Log("Draw");
    if (waveSize > 0){
      waveSize -= Time.deltaTime * straightenLineSpeed;
      moveTime = (StartWaveSize - waveSize)/StartWaveSize;
      DrawRopeWaves();
    }
    else{
      waveSize = 0;

      if (lr.positionCount != 2) lr.positionCount = 2;

      DrawRopeNoWaves();
    }
  }

  public void DrawRopeNoWaves(){
    if(lr.positionCount != 2) lr.positionCount = 2;
    Vector3 ppos = new Vector3(player.position.x, player.position.y, -1);
    Vector3 gppos = new Vector3(gp.position.x,gp.position.y,-1);
    lr.SetPosition(0, ppos);
    lr.SetPosition(1, gppos);
  }

  public void DrawRopeWaves(){
    //Debug.Log("Waves At " + ropeProgressionCurve.Evaluate(moveTime));
    for(int i = 0; i< percision;i++){
        float delta = (float) i / ((float) percision - 1f);
        Vector2 offset = Vector2.Perpendicular(((player.position - gp.position).normalized) * ropeAnimationCurve.Evaluate(delta) * waveSize);
        Vector2 targetPosition = Vector2.Lerp(player.position, gp.position, delta) + offset;
        Vector2 currentPosition = Vector2.Lerp(player.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

        Vector3 currentPos = new Vector3(currentPosition.x, currentPosition.y, -1);
        lr.SetPosition(i, currentPos);
    }
  }

  public void Update(){
//    Debug.Log(isGrappling);
    if(isGrappling) DrawRope();
  }

}