using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    private bool flashlightActive = true;
    void Start()
    {
        FlashlightLight.gameObject.SetActive(true); //bit ce aktivan kad ga prvi put pickupam u ruku
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
}
