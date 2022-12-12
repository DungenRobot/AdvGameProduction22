using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Tutorial scean");
    }
}
