using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup pauseMenu;
    bool isPauseMenuOn = false;
    [SerializeField]
    private Slider lookSensitivitySlider;
    [SerializeField]
    private TextMeshProUGUI lookSensitivityValue;
    
    public void Start()
    {
        lookSensitivitySlider.GetComponent<Slider>().onValueChanged.AddListener(lookSensitivity);
        lookSensitivity(lookSensitivitySlider.value);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("PauseMenu");
            PanelPauseMenuController();
        }
        
    }
    public void PanelPauseMenuController()
    {
        if (!isPauseMenuOn)
        {
            pauseMenu.alpha = 1;
            pauseMenu.blocksRaycasts = true;
            Time.timeScale = 0;
            isPauseMenuOn = true;
            UIController.MyInstance.TurnCursorOn();

        }
        else if (isPauseMenuOn)
        {
            pauseMenu.alpha = 0;
            pauseMenu.blocksRaycasts = false;
            Time.timeScale = 1;
            isPauseMenuOn = false;
            UIController.MyInstance.TurnCursorOff();
            
        }
    }
    public void QuitGame()
    {
        Debug.Log("QuitGame!");
        Application.Quit();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SaveOptions()
    {
        lookSensitivity(lookSensitivitySlider.value);
        Debug.Log("Saving Look Sensitivity at: " + lookSensitivitySlider.value);
    }
    public void lookSensitivity(float value)
    {
        lookSensitivitySlider.maxValue = 200;
        lookSensitivitySlider.minValue = 50;

        CameraFollow.MyInstance.MouseSensitivity = value;
        lookSensitivityValue.text = value.ToString();

    }
}
