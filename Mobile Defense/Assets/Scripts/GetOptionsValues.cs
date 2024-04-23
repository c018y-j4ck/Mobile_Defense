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
        setOptions();
    }

    // Update is called once per frame
    void Update()
    {
        setOptions();
    }

    private void setOptions()
    {
        if (PlayerPrefs.HasKey("volume%"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume%");
        }
        else
        {
            AudioListener.volume = 1;
        }
        if (PlayerPrefs.HasKey("textSize"))
        {
            int theSize = PlayerPrefs.GetInt("textSize");
            TextMeshProUGUI[] allTmpro = FindObjectsOfType<TextMeshProUGUI>();
            Debug.Log("there are " + allTmpro.Length);
            foreach (TextMeshProUGUI i in allTmpro)
            {
                i.fontSize = theSize;
            }
        }
    }
}
