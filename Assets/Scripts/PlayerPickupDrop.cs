using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{   
    public AudioSource objectDropSource;
    public AudioSource keySource;
    [SerializeField] private Transform playerCameraPosition;
    [SerializeField] private Transform playerCameraRotation;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask minigameLayerMask;
    [SerializeField] private LayerMask lightswitchLayerMask;
    [SerializeField] private LayerMask polugaLayerMask;

    [SerializeField] private float pickUpRange=2f;

    [SerializeField] private KeypadMinigame keypadMinigame;

    [SerializeField] private Transform FlashlightLocation; //desna ruka
    private objectGrabbable objectGrabbable;
    private FlashlightGrabPoint flashlightGrabbable;

    private Flashlight flashlight; //skripta da onemogucim F ako flashlight nije u ruci
    private void Start(){
        flashlight = GetComponent<Flashlight>();
        
        if (flashlight != null)
        {
            // Disable the script initially
            flashlight.enabled = false;
            Debug.Log("flashlight Script has been disabled.");
        }
        else
        {
            Debug.LogError("flashlight Script is not attached to this GameObject.");
        }
        }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)){
            if (objectGrabbable == null){ //pokusavamo uzeti
                if(Physics.Raycast(playerCameraPosition.position, playerCameraRotation.forward, out RaycastHit raycastHit,pickUpRange, pickUpLayerMask)){
                    Debug.Log(raycastHit.transform.name);
                    if(raycastHit.transform.TryGetComponent(out objectGrabbable)){
                        objectGrabbable.Grab(objectGrabPointTransform); //postavljanje objekta u lijevu ruku
                        Debug.Log(objectGrabbable);
                        if (raycastHit.transform.CompareTag("Key") && keySource != null)
                        {
                            keySource.Play();
                        }
                    }
                    else if(raycastHit.transform.TryGetComponent(out flashlightGrabbable)){
                        flashlightGrabbable.Grab(FlashlightLocation); //postavljanje objekta u desnu ruku
                        flashlight.enabled=true;
                        Debug.Log("Flashlight in hand!");
                        Debug.Log(flashlightGrabbable);
                    }
           }
        } 
        else{
            //dropamo
            DropObject();
        }
        if (Physics.Raycast(playerCameraPosition.position, playerCameraRotation.forward, out RaycastHit raycastHit2, pickUpRange, minigameLayerMask)) {
                Debug.Log(raycastHit2.transform.name);

                if (raycastHit2.transform.name == "RawImage"){
                    Transform parentTransform = raycastHit2.transform.parent; //trazimo roditelja(folder)
                

                MinigameStart minigameStart = parentTransform.GetComponent<MinigameStart>();

                if (minigameStart != null && !minigameStart.isGameStarted()) {
                    minigameStart.StartMinigame();  // pokrecemo minigame
                    UnityEngine.Debug.Log("Minigame pokrenut!");
                 }
                }
                else if (raycastHit2.transform.name == "VentGame"){
                    VentGame ventGame = raycastHit2.transform.GetComponent<VentGame>();
                    if (!ventGame.isGameStarted()){
                    ventGame.StartMinigame();
                    }
                }
                else if (raycastHit2.transform.name == "KeypadTrigger") {
                    if (!keypadMinigame.isGameStarted()) //raycasta samo ako minigame nije pokrenut
                        {
                       // keypadMinigame.StartMinigame();
                        keypadMinigame.Invoke("StartMinigame", 0f);
                    //    keySource.Play();
                        }
                    else
                        {
                        Debug.Log("Minigame vec radi pa raycast nije u funkciji");
                        }
            }
                
            }
            else if (Physics.Raycast(playerCameraPosition.position, playerCameraRotation.forward, out RaycastHit raycastHit4, pickUpRange, polugaLayerMask))
        {
            Debug.Log("Hit Poluga " + raycastHit4.transform.name);

            if (raycastHit4.transform.TryGetComponent(out PolugaScript polugaScript))
                {
                polugaScript.ActivateTrigger(); //akivacija funkcije skripte na trigeru
                Debug.Log("Poluga activated.");
                }
            else
                {
                Debug.LogWarning("PolugaScript not found on hit object.");
                }
        }
            else if (Physics.Raycast(playerCameraPosition.position, playerCameraRotation.forward, out RaycastHit raycastHit3, pickUpRange))
        {
            Debug.Log("Hit lightswitch: " + raycastHit3.transform.name);

            // Provjerite ima li objekt komponentu za upravljanje svjetlima
            if (raycastHit3.transform.TryGetComponent(out LightSwitch lightSwitch))
            {
                lightSwitch.ToggleLights(); // Aktivirajte funkciju za paljenje/gašenje svjetla
                Debug.Log("Lights toggled.");
            }
        }
            
        }
    }
    public void DropObject()
    {
        if (objectGrabbable != null)
        {
            objectGrabbable.Drop();
            objectDropSource.Play();
            objectGrabbable = null;
            Debug.Log("Object dropped.");
        }
    }
}
