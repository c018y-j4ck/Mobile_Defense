using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider theSlider;
    static float currentVol;
    static float currentSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeVol()
    {
        AudioListener.volume = theSlider.value;
        PlayerPrefs.SetFloat("volume%", currentSize);
        currentVol = theSlider.value;
    }

    public void ChangeTextSize()
    {
        currentSize = (float)theSlider.value/100;
        PlayerPrefs.SetFloat("text%",currentSize);
        Text[] allText = FindObjectsOfType<Text>();
        foreach(Text i in allText)
        {
            i.fontSize = (int)currentSize*i.fontSize;
        }
    }
}
