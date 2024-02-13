using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuButtonScript : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject OptionsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lvl1Enter()
    {
        //scene change to lvl1
    }

    public void lvl2Enter()
    {
        //scene change to lvl2
    }

    public void OptionsEnter()
    {
        OptionsCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        
    }

    public void BackEnter()
    {
        MenuCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);
         
    }

    public void QuitEnter()
    {
        Application.Quit();
    }
}
