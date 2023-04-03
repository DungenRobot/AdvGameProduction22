using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class pausemenu : MonoBehaviour
{
    public GameObject pausemenuOBJ;
    public GameObject settingmenuOBJ;
    public bool onpaused;
    public GameObject Music;
    public bool g0;
    public Slider TextSlider;

    // Start is called before the first frame update
    void Start()
    {
        //Starting rules for stuff.
        Music = GameObject.Find("Music");
        onpaused = false;
        pausemenuOBJ.SetActive(false);
        g0 = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        TextSlider.onValueChanged.AddListener(delegate {onTextSizeValueChanged();});
    }

    // Update is called once per frame
    void Update()
    {
        if (g0)
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
    }
    //To pause Game
    public void pause()
    {
        onpaused = true;
        pausemenuOBJ.SetActive(true);
        settingmenuOBJ.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //To resume game
    public void resume()
    {
        onpaused = false;
        pausemenuOBJ.SetActive(false);
        settingmenuOBJ.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ResetTheLevel()
    {
        Destroy(Music);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
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
        Destroy(Music);
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
//Charlotte things : )
    //Text Slider Stuff


    Dictionary<int, float> PrevFontSizes = new Dictionary<int, float>();

    public void onTextSizeValueChanged()
    {
        //Finds all Text Objects that are TextMeshPro
        TextMeshProUGUI[] textObjs = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        Debug.Log(textObjs.Length);
            //This refrences all the Text for the specific objects
            foreach(TextMeshProUGUI textObj in textObjs){
                //
            float PrevFontSize = 0;
            if(!PrevFontSizes.ContainsKey(textObj.gameObject.GetInstanceID())){
                PrevFontSizes.Add(textObj.gameObject.GetInstanceID(), textObj.fontSize);
                PrevFontSize = textObj.fontSize;
            }else PrevFontSize = PrevFontSizes[textObj.gameObject.GetInstanceID()];
            textObj.fontSize = PrevFontSize * TextSlider.value * 2;
        }
   }
}