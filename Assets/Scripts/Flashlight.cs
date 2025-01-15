using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    private bool flashlightActive = false;
    void Start()
    {
        FlashlightLight.gameObject.SetActive(false); //nece biti aktivan kad ga prvi put pickupam
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            if(flashlightActive==false){
                FlashlightLight.gameObject.SetActive(true);
                flashlightActive=true;
            }
            else{
                flashlightActive=false;
                FlashlightLight.gameObject.SetActive(false);
            }
        }
    }
    public bool isFlashlightActive(){
        return flashlightActive;
    }
}
