using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class muzikaController : MonoBehaviour
{
    public GameObject player;
    public AudioSource muzikaSource;
    public AudioSource chaseMuzikaSource;
    public GameObject cudoviste;
    public GameObject drugoCudoviste;

    private bool first;

    void Start()
    {
        first=true;
    }


    void Update()
    {
        if(isPlayerInSafeRoom()&&first){
            chaseMuzikaSource.Pause();
            muzikaSource.Play();
            drugoCudoviste.SetActive(false);
            AudioSource[] audioSources = cudoviste.GetComponents<AudioSource>();
            foreach (AudioSource audio in audioSources)
            {
                audio.mute = false;
            }
            first=false;
        }
    }




    public bool isPlayerInSafeRoom()
    {
        bool safe = player.transform.position.x >= -83 && player.transform.position.x < -76 && player.transform.position.z >= -1 && player.transform.position.z <= 10;
        return safe;
    }
}
