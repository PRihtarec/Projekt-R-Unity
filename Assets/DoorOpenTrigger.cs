using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private objectGrabbable Key;

    private PlayerPickupDrop playerPickupDrop;

    private void Start()
    {
        playerPickupDrop = FindObjectOfType<PlayerPickupDrop>();

    }
    private void OnTriggerEnter(Collider other){
        Debug.Log("Kljuc u u ključanici");
        if (other.CompareTag("Key")){
            door.open();
            objectGrabbable key = other.GetComponent<objectGrabbable>(); //kad se vrata otkljucaju dropam key na pod
            playerPickupDrop.DropObject();
        }
    }
}
