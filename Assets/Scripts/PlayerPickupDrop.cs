using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{   
    [SerializeField] private Transform playerCameraPosition;
    [SerializeField] private Transform playerCameraRotation;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private float pickUpRange=2f;
    private objectGrabbable objectGrabbable;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)){
            if (objectGrabbable == null){ //pokusavamo uzeti
                if(Physics.Raycast(playerCameraPosition.position, playerCameraRotation.forward, out RaycastHit raycastHit,pickUpRange, pickUpLayerMask)){
                    Debug.Log(raycastHit);
                    if(raycastHit.transform.TryGetComponent(out objectGrabbable)){
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);
                }
           }
        } else{
            //dropamo
            objectGrabbable.Drop();
            objectGrabbable = null;
        }
        }
    }
}
