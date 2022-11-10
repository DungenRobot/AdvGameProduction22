using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tricks : MonoBehaviour
{
    public Trick[] tricks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable] public struct Trick{
        public string tag;
        public Vector2 maxDirToTarget; // The farthest direction you can be away from the target obj (tag)
        public Vector2 minDirToTarget; // The shortest direction you can be away from the target obj (tag)
        public bool orderedKey; // Press keys quick in order or have them all down at once
        public float keyPressSpeed; // The speed all keys have to be pressed down in, for ordered keys and unordered
        public float keyPressSpeedError; // The allowed time off your are abble to be from the key press speed

        public KeyCode[] keys;

        // Score stuff wooo
    }
}
