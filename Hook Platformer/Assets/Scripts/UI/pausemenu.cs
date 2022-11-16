using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    public GameObject pausemenuOBJ;
    public GameObject settingmenuOBJ;
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
        settingmenuOBJ.SetActive(false);
    }
    //To resume game
    public void resume()
    {
        onpaused = false;
        pausemenuOBJ.SetActive(false);
        settingmenuOBJ.SetActive(false);

    }

    //To Open the Setting Menu
    public void settingopen()
    {
        settingmenuOBJ.SetActive(true);
        pausemenuOBJ.SetActive(false);
    }

    public void settingclose()
    {
        settingmenuOBJ.SetActive(false);
        pausemenuOBJ.SetActive(true);
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

    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //This is for the options menu

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Resolution1920()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    public void Resolution1280()
    {
        Screen.SetResolution( 1280, 800, true);
    }

    public void Resolution640()
    {
        Screen.SetResolution(640, 480, true);
    }
    }
