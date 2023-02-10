using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class TranslationText : MonoBehaviour
{
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        LanguageManager.getInstance().AddTranslationText(this);
    }
    
    public void SetText(string text){
        this.GetComponent<TextMeshProUGUI>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
