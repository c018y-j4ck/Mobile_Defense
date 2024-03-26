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
    public GameObject instructionsCanvas;

    public GameObject TurretCanvas;
    public GameObject TurretButton;

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
        SceneManager.LoadScene("Level_1");
    }

    public void lvl2Enter()
    {
        SceneManager.LoadScene("Level_2");
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
        instructionsCanvas.SetActive(false);
         
    }

    public void InstructionsEnter()
    {
        instructionsCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
    }

    public void DefaultEnter()
    {
       Slider volumeSlide= GameObject.Find("VolSlider").GetComponent<Slider>();
        volumeSlide.value = 1;
        Slider textSlide = GameObject.Find("TextSlider").GetComponent<Slider>();
        textSlide.value = 36;
    }

    public void QuitEnter()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void MenuEnter()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenTurretMenu()
    {
        TurretCanvas.SetActive(true);
        TurretButton.SetActive(false);
    }

    public void CloseTurretMenu()
    {
        TurretCanvas.SetActive(false);
        TurretButton.SetActive(true);
    }
}
