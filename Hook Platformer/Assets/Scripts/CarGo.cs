using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGo : MonoBehaviour
{
    private float rTime;
    // Start is called before the first frame update
    void Start()
    {
        rTime = Random.Range(1.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rTime = rTime - Time.deltaTime;
        if (rTime == 0)
        {
            SpawnCar();
        }
    }

    void SpawnCar()
    {
        rTime = Random.Range(1.0f, 10.0f);
    }
}
