using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRope : MonoBehaviour{
    public LineRenderer lr;
    List<Mass> masses;
    List<Spring> springs;
    public int numSections = 5;
    public Vector2 gravity = new Vector2(0,9.8f);

    public Transform ropeConnection1;
    public Transform ropeConnection2;


    public float springConstant;
    public float springLength;
    public float frictionConstant;
/*
    float groundRepulsionConstant;
    float groundFrictionConstant;
    float groundAbsorptionConstant;
    float groundHeight;
    float airFrictionConstant; 
*/

    public void Start(){
        masses = new List<Mass>();
        springs = new List<Spring>();

        Vector2 singleSeg = (ropeConnection1.position - ropeConnection2.position) / numSections;

    Vector2 pos1 = ropeConnection1.position;
        for(int i = 0; i<numSections+1; i++)
            masses.Add(new Mass(singleSeg * i + pos1));

        for(int i = 0; i<numSections;i++)
            springs.Add(new Spring(masses[i], masses[i+1], springConstant, 1, 1));
    }

    public void Update(){
        masses[0].position = ropeConnection1.position;
        masses[masses.Count - 1].position = ropeConnection2.position;

        Vector2 singleSeg = (ropeConnection1.position - ropeConnection2.position) / numSections;
        foreach(Spring spring in springs) spring.springLength = singleSeg.magnitude;

        Simulate();
        Vector3[] positions = new Vector3[masses.Count];
        for(int i = 0; i<masses.Count;i++){
            positions[i] = masses[i].position;
        }
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }

    public void Simulate(){
        foreach(Spring spring in springs){
            spring.Simulate();
        }
    }

    public class Mass{
        public Vector2 position = Vector2.zero; 
        public Vector2 velocity = Vector2.zero;
        public Vector2 acceleration = Vector2.zero;

        public Mass(Vector2 position){
            this.position = position;
        }
        public void ApplyForce(Vector2 force){
            velocity = velocity + force;
         //   acceleration = acceleration + force;
        }

        public void Simulate(){
        //    acceleration = Vector2.Lerp(acceleration, Vector2.zero, 0.05f);
        //    velocity = velocity + (acceleration + new Vector2(0,9.8f)) * Time.deltaTime;
            position = position + (velocity + new Vector2(0,-9.8f)) * Time.deltaTime;
     //       Debug.Log(acceleration);
          Debug.Log(velocity);
    //        Debug.Log(position);
        }
    }

    public class Spring
    {
        Mass mass1;
        Mass mass2;

        public float springLength;
        public float frictionConstant;
        public float springConstant;
        public float stiffness = 0.2f;

        public Spring(Mass mass1, Mass mass2, float springConstant, float springLength, float frictionConstant){
            this.mass1 = mass1;
            this.mass2 = mass2;

            this.springConstant = springConstant;
            this.springLength = springLength;
            this.frictionConstant = frictionConstant;
        }

        public void Simulate(){
            Vector2 springVector = mass1.position - mass2.position; // Vector between 2 objs
            float r = springVector.magnitude; // Dist between 2 objs

            Vector2 force = Vector2.zero;

        //    if(r != 0)
            force = -springConstant * (r-springLength) * (springVector.normalized) - (0.5f * (mass1.velocity - mass2.velocity));
                //force += -(springVector / r) * (r - springLength) * springConstant;


           // force += -(mass1.velocity - mass2.velocity) * frictionConstant;

            mass1.ApplyForce(force);
            mass2.ApplyForce(-force);

            mass1.Simulate();
            mass2.Simulate();
        }
      
    }

}