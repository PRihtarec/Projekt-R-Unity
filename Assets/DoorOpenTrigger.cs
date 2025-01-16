using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private objectGrabbable Key;
    private void OnTriggerEnter(Collider other){
        Debug.Log("Kljuc u u ključanici");
        if (other.CompareTag("Key")){
            door.open();
            objectGrabbable key = other.GetComponent<objectGrabbable>();
            key.Drop();
        }
    }
}
