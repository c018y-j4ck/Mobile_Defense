using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetOptionsValues : MonoBehaviour
{
    float fontSize;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume%");
        //if (PlayerPrefs.GetFloat("text%") == 0f)
        //{
        //    fontSize = 1f;
        //}
        Text[] allText = FindObjectsOfType<Text>();
        foreach (Text i in allText)
        {
            i.fontSize = (int)PlayerPrefs.GetFloat("text%") * i.fontSize;
        }
        //TextMeshProUGUI[] allTMP = FindObjectsOfType<TextMeshProUGUI>();
        //foreach (TextMeshProUGUI i in allTMP)
        //{
        //    i.fontSize = (int)PlayerPrefs.GetFloat("text%") * i.fontSize;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume%");
        Text[] allText = FindObjectsOfType<Text>();
        foreach (Text i in allText)
        {
            i.fontSize = (int)PlayerPrefs.GetFloat("text%") * i.fontSize;
        }
        //TextMeshProUGUI[] allTMP = FindObjectsOfType<TextMeshProUGUI>();
        //foreach (TextMeshProUGUI i in allTMP)
        //{
        //    i.fontSize = (int)PlayerPrefs.GetFloat("text%") * i.fontSize;
        //}
    }
}
