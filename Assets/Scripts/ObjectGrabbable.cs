using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class objectGrabbable : MonoBehaviour
{   
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    
    private void Awake() {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void Grab (Transform objectGrabPointTransform){
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.isKinematic = true;
    }
    public void Drop(){
        this.objectGrabPointTransform=null;
        objectRigidbody.useGravity = true;
        objectRigidbody.isKinematic = false;
    }
    private void FixedUpdate() {
        if (objectGrabPointTransform != null){
            float lerpSpeed = 20f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            transform.position = newPosition;

            Quaternion targetRotation = objectGrabPointTransform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        }
    }
}
