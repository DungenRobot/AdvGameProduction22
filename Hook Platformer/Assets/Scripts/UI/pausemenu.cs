using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    public GameObject pausemenuOBJ;
    public bool onpaused;
    // Start is called before the first frame update
    void Start()
    {
        //Starting rules for stuff.
        onpaused = false;
        pausemenuOBJ.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Input for pausing/resuming the game using the escape key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(onpaused)
            {
                resume();
            }

            else
            {
                pause();
            }
        }
    }
    //To pause Game
    public void pause()
    {
        onpaused = true;
        pausemenuOBJ.SetActive(true);
    }
    //To resume game
    public void resume()
    {
        onpaused = false;
        pausemenuOBJ.SetActive(false);

    }
    //To Quit Game
    public void quit()
    {
        Application.Quit();
    }

    public void sceneswitch()
    {
        //Basic Command to switch scenes *Scenelessly* : )
        //Ask Preston for further information and hit him
        //for that pun
        SceneManager.LoadScene("Collision Basics");
    }
}
