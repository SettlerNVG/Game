using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject menuPanel;
    public GameObject controlUI;
    public GameObject textAppearing;
    //Canvas canvas;

    ///Главное меню игры
    public void PlayGame()
    {
        Application.LoadLevel("FirstLevel");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    ///


    //Меню в игре
    public void ExitSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
    public void ButtonMenuInGame()
    {
        //canvas.sortingOrder = 1;
        if(menuPanel!=null)
        {
            textAppearing.SetActive(false);
            controlUI.SetActive(false);
            menuPanel.SetActive(true);
            Time.timeScale = 0f;//остановка игры при паузе
        }
    }
    public void OutFromMenu()
    {
        controlUI.SetActive(true);
        menuPanel.SetActive(false);

        Time.timeScale = 1f;
    }
    public void buttonRestartLevel()
    {
        SceneManager.LoadScene("FirstLevel");
        Time.timeScale = 1f;
    }
    public void ControlsUI()
    {
        controlUI.SetActive(false);
    }

    //
}
