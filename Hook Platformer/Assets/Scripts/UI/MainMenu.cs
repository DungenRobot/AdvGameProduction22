using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int latestLevel;
    public GameObject MainM;
    public GameObject LicenceM;
    public GameObject GameSelect;



    public void Start()
    {
        MainM.SetActive(true);
        LicenceM.SetActive(false);
        GameSelect.SetActive(false);
        latestLevel = GameObject.Find("DataHandler").GetComponent<DataHandler>().LatestLevel;

    }
    public void quit()
    {
        Application.Quit();
    }

    public void PlayTutorial()
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

    //GameSelect Code

    public void Ls()
    {
        GameSelect.SetActive(true);
        MainM.SetActive(false);
    }

    public void Bls()
    {
        GameSelect.SetActive(false);
        MainM.SetActive(true);
    }



    //Licenses Code for the Menu

    
}
