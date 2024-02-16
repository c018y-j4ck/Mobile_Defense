using System.Collections;
using System.Collections.Generic;
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
        
    }

    public void ChangeVol()
    {
        AudioListener.volume = theSlider.value;
        currentVol = theSlider.value;
    }

    public void ChangeTextSize()
    {
        currentSize = (int)theSlider.value;
        Text[] allText = FindObjectsOfType<Text>();
        foreach(Text i in allText)
        {
            i.fontSize = currentSize;
        }
    }
}
