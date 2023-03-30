using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
public class LanguageManager : MonoBehaviour
{
    private static LanguageManager inst;
    public TextAsset TextDict;

    public static LanguageManager getInstance(){
        return inst;
    }

    public List<TranslationText> textObjects;

    public void AddTranslationText(TranslationText text){
        textObjects.Add(text);
        Debug.Log(texts[text.id]);
        text.SetText(texts[text.id][language]);
    }

    public string language;
    public void SetLanguage(string languageTag){
        this.language = languageTag;

        // Update All Objects
        textObjects.ForEach(textObject => {
            textObject.SetText(texts[textObject.id][language]);
        });
    }

    private Dictionary<string,Dictionary<string, string>> texts; // text title, (language, translated text)

    // Start is called before the first frame update
    void Start()
    {
        if(inst != null){
            Destroy(inst);
        }
        inst = this;
        DontDestroyOnLoad(this);
         SceneManager.sceneLoaded += OnSceneLoaded;

        texts = JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string, string>>>(TextDict.text);
        //JsonUtility.FromJson<Texts>(TextDict.text);
    }

     public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {
        SetLanguage(this.language); // Reset the language of all the new text in the scene
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
