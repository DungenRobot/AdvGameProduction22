using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class gameOver : MonoBehaviour
{
    public pausemenu script;
    public GameObject game0ver;

    void Start()
    {
        game0ver.SetActive(false);
    }

    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            script.g0 = false;
            game0ver.SetActive(true);
        }
        
    }

    void menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
