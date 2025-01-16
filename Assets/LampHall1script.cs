using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHall1Script : MonoBehaviour
{
    [SerializeField] private Animator NeonLight;
    private void OnTriggerEnter(Collider other){
        Debug.Log("usao si u triger");
        if (other.CompareTag("player")){
            NeonLight.SetTrigger("flicker");
        }
    }
}
