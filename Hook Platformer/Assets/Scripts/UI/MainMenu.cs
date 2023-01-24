using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject MainM;
    public GameObject LicenceM;



    public void Start()
    {
        MainM.SetActive(true);
        LicenceM.SetActive(false);
    }
    public void quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Tutorial scean");
        Time.timeScale = 1;
    }

    public void Lm()
    {
        MainM.SetActive(false);
        LicenceM.SetActive(true);
    }

    public void Blm()
    {
        MainM.SetActive(true);
        LicenceM.SetActive(false);
    }
}
