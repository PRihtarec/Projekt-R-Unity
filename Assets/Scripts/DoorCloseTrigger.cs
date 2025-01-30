using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private GameObject unableChaseScene;

    private bool alreadyTriggered = false;
    private void OnTriggerEnter(Collider other){
        Debug.Log("usao si u trigger za zatvaranje vrata - DoorCloseTrigger");
        if (alreadyTriggered) return;
        
        if (other.CompareTag("player")){
            door.close();
            alreadyTriggered = true;
            if (unableChaseScene!= null)
            unableChaseScene.SetActive(false);
            
        }
    }
}
