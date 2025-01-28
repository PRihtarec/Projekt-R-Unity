using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public Options optionsScript;
    public MonoBehaviour cameraScript;
    public AudioSource mainMenuSource;
    private AudioSource[] allAudioSources; 
    public GameObject tockica;
    void Start(){
        allAudioSources = FindObjectsOfType<AudioSource>();
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            optionsScript.LoadVolume();
        }
        if (PlayerPrefs.HasKey("mouseSensitivity"))
        {
            optionsScript.LoadMouse();
        }
    
    }
    public void PlayGame(){
        SceneManager.LoadScene("Game");
    }
    public void ResumeGame(){
        Time.timeScale = 1;        
        cameraScript.enabled = true;            
        Cursor.lockState = CursorLockMode.Locked;     
        Cursor.visible = false;                      
        mainMenuPanel.SetActive(false);
        Canvas.ForceUpdateCanvases();
        tockica.SetActive(true);
        mainMenuSource.Stop();

        foreach (var audioSource in allAudioSources)
        {
            if (audioSource.mute == true)
            {
                audioSource.mute = false;
            }
        }
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void OptionsMenu(){
        optionsPanel.SetActive(true);
        Canvas.ForceUpdateCanvases();
        mainMenuPanel.SetActive(false);
    }
}
