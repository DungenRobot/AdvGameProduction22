using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class gameOver : MonoBehaviour
{
    public pausemenu script;
    public GameObject game0ver;
    public GameObject Music;
    public AudioSource defeatas;
    public GameObject defeat;
    

    void Start()
    {
        defeat = GameObject.Find("defeat");
        defeatas = defeat.GetComponent<AudioSource>();
        Music = GameObject.Find("Music");
        game0ver.SetActive(false);
        Destroy(Music);
        defeatas.Play();
    }

    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Time.timeScale = 0f;
            script.g0 = false;
            game0ver.SetActive(true);
        }
        
    }

    public void menu()
    {
        Destroy(Music);
        SceneManager.LoadScene("MainMenu");
    }
    
    public void tryAgain()
    {
        Destroy(Music);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
