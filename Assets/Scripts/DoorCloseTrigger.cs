using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private GameObject Destroyfirstroom;
    private void OnTriggerEnter(Collider other){
        Debug.Log("usao si u triger");
        if (other.CompareTag("player")){
            door.close();
            Destroy(Destroyfirstroom);
        }
    }
}
