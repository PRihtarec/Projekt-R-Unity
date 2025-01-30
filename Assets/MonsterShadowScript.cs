using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShadowScript : MonoBehaviour
{
    public AudioSource roarSource;
    public AudioSource muzikaSource;
    public AudioSource chaseMuzikaSource;
    [SerializeField] private Animator MonsterShadow;

    private bool alreadyTriggered = false;
    private void OnTriggerEnter(Collider other){
        Debug.Log("monster flash");

        if (alreadyTriggered) return;

        if (other.CompareTag("player")){
            MonsterShadow.SetTrigger("flicker");
            roarSource.Play();
            muzikaSource.Stop();
            chaseMuzikaSource.Play();
            alreadyTriggered = true;
        }
    }
}

