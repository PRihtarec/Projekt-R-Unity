using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void OptionsMenu(){
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
