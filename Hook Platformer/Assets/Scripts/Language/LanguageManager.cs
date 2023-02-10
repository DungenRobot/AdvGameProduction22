using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private static LanguageManager inst;
    public TextAsset TextDict;

    class TextEntry{
        
    }

    public static LanguageManager getInstance(){
        return inst;
    }

    public List<TranslationText> textObjects;

    public void AddTranslationText(TranslationText text){
        textObjects.Add(text);
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
        
        //JsonUtility.FromJson<Texts>(TextDict.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
