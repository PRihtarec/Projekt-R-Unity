using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShadowScript : MonoBehaviour
{
    public AudioSource roarSource;
    [SerializeField] private Animator MonsterShadow;
    private void OnTriggerEnter(Collider other){
        Debug.Log("monster flash");
        if (other.CompareTag("player")){
            MonsterShadow.SetTrigger("flicker");
            roarSource.Play();
        }
    }
}

