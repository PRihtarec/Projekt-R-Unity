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
    private bool isPaused;
    void Start(){
        isPaused=false;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame(); 
            }
        }
    }
    public void PlayGame(){
        SceneManager.LoadScene("Game");
    }
    void PauseGame()
    {
        Time.timeScale = 0;                
        cameraScript.enabled = false;        
        Cursor.lockState = CursorLockMode.None;   
        Cursor.visible = true;                
        mainMenuPanel.SetActive(true); 
        optionsPanel.SetActive(false);
        Canvas.ForceUpdateCanvases();
        tockica.SetActive(false);
        foreach (var audioSource in allAudioSources)
        {
            if (audioSource.mute==true)
            {
                audioSource.mute=false;
            }
            else
            {
                audioSource.mute=true;
            }
        }
        isPaused=true;
    }
    public void ResumeGame(){
        Time.timeScale = 1;        
        cameraScript.enabled = true;            
        Cursor.lockState = CursorLockMode.Locked;     
        Cursor.visible = false;                      
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        Canvas.ForceUpdateCanvases();
        tockica.SetActive(true);

        foreach (var audioSource in allAudioSources)
        {
            if (audioSource.mute==false)
            {
                audioSource.mute=true;
            }
            else
            {
                audioSource.mute=false;
            }
        }
        isPaused=false;
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
