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
    GamesController gamesController;
    void Start(){
        gamesController = FindObjectOfType<GamesController>();
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
        Cursor.visible = true; //postavljam cursor da bude vidljiv, fixa problem s cursorom nakon sto player umre ili pobijedi
        Cursor.lockState = CursorLockMode.None;
    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gamesController.isMinigameInProgress()){
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
    }
    public void PlayGame(){
        Cursor.lockState = CursorLockMode.None;     
        Cursor.visible = false;
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
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        mainMenuSource.Play();
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
        mainMenuSource.Stop();

        foreach (var audioSource in allAudioSources)
        {
            if (audioSource!=mainMenuSource)
            {
                audioSource.UnPause();
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
