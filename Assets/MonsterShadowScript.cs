using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShadowScript : MonoBehaviour
{
    [SerializeField] private Animator MonsterShadow;
    private void OnTriggerEnter(Collider other){
        Debug.Log("usao si u triger");
        if (other.CompareTag("player")){
            MonsterShadow.SetTrigger("flicker");
        }
    }
}

