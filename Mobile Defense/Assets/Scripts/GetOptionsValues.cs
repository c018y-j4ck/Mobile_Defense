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
        if (PlayerPrefs.HasKey("textSize%"))
        {
            float theSize = PlayerPrefs.GetFloat("textSize%");
            TextMeshProUGUI[] allTmpro = FindObjectsOfType<TextMeshProUGUI>();
            Debug.Log("there are " + allTmpro.Length);
            foreach (TextMeshProUGUI i in allTmpro)
            {
                if (!i.gameObject.tag.Equals("InstrucText"))
                {
                    if (i.gameObject.tag.Equals("TurretTopText"))
                    {
                        i.fontSize = (int)5 * theSize;
                    }
                    else if (i.gameObject.tag.Equals("TurretButtonText"))
                    {
                        i.fontSize = (int)12 * theSize;
                    }
                    else if (i.gameObject.tag.Equals("TurretMenuText"))
                    {
                        i.fontSize = (int)15 * theSize;
                    }
                    else
                    {
                        i.fontSize = (int)50 * theSize;
                    }
                }
            }
        }
    }
}
