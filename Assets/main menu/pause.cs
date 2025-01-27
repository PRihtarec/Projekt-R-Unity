using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public MonoBehaviour cameraScript;
    public AudioSource mainMenuSource;
    public GameObject tockica;

    private AudioSource[] allAudioSources; 
    void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            Time.timeScale = 0;        
            cameraScript.enabled = false;            
            Cursor.lockState = CursorLockMode.None;     
            Cursor.visible = true;                      
            pauseMenuCanvas.SetActive(true);
            Canvas.ForceUpdateCanvases();
            tockica.SetActive(false);
            foreach (var audioSource in allAudioSources)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.mute = true;
                }
            }
            mainMenuSource.Play();
        }
        
    }
}
