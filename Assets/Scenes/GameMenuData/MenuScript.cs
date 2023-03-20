using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject menuPanel;
    //Canvas canvas;
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
    public void ExitSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
    public void ButtonMenuInGame()
    {
        //canvas.sortingOrder = 1;
        if(menuPanel!=null)
        {
            menuPanel.SetActive(true);
        }
    }
    public void OutFromMenu()
    {
        menuPanel.SetActive(false);
    }
}
