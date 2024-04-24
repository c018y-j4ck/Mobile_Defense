using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider theSlider;
    static float currentVol;
    static int currentSize;

    // Start is called before the first frame update
    void Start()
    {
        if (theSlider.CompareTag("vol"))
        {
            theSlider.value = PlayerPrefs.GetFloat("volume%");
        }
        else
        {
            theSlider.value = PlayerPrefs.GetInt("textSize");
        }
    }

    public void ChangeVol()
    {
        AudioListener.volume = theSlider.value;
        currentVol = AudioListener.volume;
        PlayerPrefs.SetFloat("volume%", currentVol);
    }

    public void ChangeTextSize()
    {
        float currentSize = theSlider.value;
        PlayerPrefs.SetFloat("textSize%",currentSize);
        Text[] allText = FindObjectsOfType<Text>();
        foreach(Text i in allText)
        {
            i.fontSize = PlayerPrefs.GetInt("textSize%");
        }
        //TextMeshProUGUI[] allTMP = FindObjectsOfType<TextMeshProUGUI>();
        //foreach (TextMeshProUGUI i in allTMP)
        //{
        //    i.fontSize = (int)PlayerPrefs.GetFloat("text%") * i.fontSize;
        //}
    }
}
