using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider mouseSlider;
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
        if (PlayerPrefs.HasKey("mouseSensitivity"))
        {
            volumeSlider.value=PlayerPrefs.GetFloat("mouseSensitivity");
        }
        else
        {
            volumeSlider.value=0.5f;
        }
    }
    public void Back(){
        mainMenuPanel.SetActive(true);
        Canvas.ForceUpdateCanvases();
        optionsPanel.SetActive(false);
    }
    public void SetVolume()
    {
        float volume= volumeSlider.value;
        mixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void LoadVolume(){
        volumeSlider.value=PlayerPrefs.GetFloat("musicVolume");
        mixer.SetFloat("music", Mathf.Log10(volumeSlider.value)*20);
    }

    public void SetMouse(){
        float mouse= mouseSlider.value;
        PlayerPrefs.SetFloat("mouseSensitivity", mouse);
        PlayerPrefs.Save();
    }
    public void LoadMouse(){
        mouseSlider.value=PlayerPrefs.GetFloat("mouseSensitivity");
    }
}
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
