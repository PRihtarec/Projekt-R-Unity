using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class muzika : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    void Start(){
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float volume= PlayerPrefs.GetFloat("musicVolume");
            mixer.SetFloat("music", Mathf.Log10(volume)*20);
        }
    
    }
}
