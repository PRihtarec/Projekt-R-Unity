using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightGrabPoint : MonoBehaviour
{   
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;

    [SerializeField] private GameObject Flashlight;
    
    private void Awake() {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void Grab (Transform objectGrabPointTransform){
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.isKinematic = true;
        
    }
    private void FixedUpdate() {
        if (objectGrabPointTransform != null){
            float lerpSpeed = 20f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            transform.position = newPosition;

            // Dobivanje ciljne rotacije (samo za rotaciju oko Y osovine)
        Quaternion targetRotation = objectGrabPointTransform.rotation;

        // Postavljanje rotacije tako da X os bude okrenuta prema naprijed
        Vector3 forwardDirection = objectGrabPointTransform.forward; // usmjerenje prema naprijed
        Quaternion targetRotationWithForward = Quaternion.LookRotation(forwardDirection, objectGrabPointTransform.up);

        // Primjena rotacije uz interpolaciju
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationWithForward, Time.deltaTime * lerpSpeed);
        Flashlight.SetActive(true);
        Destroy(gameObject);

    }
}
}

