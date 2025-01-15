using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public Slider volumeSlider;
    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }
    }
    public void Back(){
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
