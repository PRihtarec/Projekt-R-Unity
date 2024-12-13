using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    private bool flashlightActive = false;
    // Start is called before the first frame update
    void Start()
    {
        FlashlightLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
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
