using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{   
    [SerializeField] private Transform playerCameraPosition;
    [SerializeField] private Transform playerCameraRotation;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask minigameLayerMask;
    [SerializeField] private float pickUpRange=2f;

    [SerializeField] private Transform FlashlightLocation; //desna ruka
    private objectGrabbable objectGrabbable;
    private FlashlightGrabPoint flashlightGrabbable;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)){
            if (objectGrabbable == null){ //pokusavamo uzeti
                if(Physics.Raycast(playerCameraPosition.position, playerCameraRotation.forward, out RaycastHit raycastHit,pickUpRange, pickUpLayerMask)){
                    Debug.Log(raycastHit.transform.name);
                    if(raycastHit.transform.TryGetComponent(out objectGrabbable)){
                        objectGrabbable.Grab(objectGrabPointTransform); //postavljanje objekta u lijevu ruku
                        Debug.Log(objectGrabbable);
                    }
                    if(raycastHit.transform.TryGetComponent(out flashlightGrabbable)){
                        flashlightGrabbable.Grab(FlashlightLocation); //postavljanje objekta u desnu ruku
                        Debug.Log(flashlightGrabbable);
                    }
           }
        } 
        else{
            //dropamo
            objectGrabbable.Drop();
            objectGrabbable = null;
        }
        if (Physics.Raycast(playerCameraPosition.position, playerCameraRotation.forward, out RaycastHit raycastHit2, pickUpRange, minigameLayerMask)) {
                Debug.Log(raycastHit2.transform.name);

                Transform parentTransform = raycastHit2.transform.parent; //trazimo roditelja(folder)

                MinigameStart minigameStart = parentTransform.GetComponent<MinigameStart>();

                if (minigameStart != null && !minigameStart.gameStarted) {
                    minigameStart.StartMinigame();  // pokrecemo minigame
                    Debug.Log("Minigame pokrenut!");
                 }
            }
        }
    }
}
