using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private Light[] lights; //svjetla koja prekidač kontrolira
    [SerializeField] private GameObject switchOnVisual;
    [SerializeField] private GameObject switchOffVisual;

    [SerializeField] private MeshRenderer meshRenderer; // Komponenta koja upravlja prikazom objekta
    [SerializeField] private MeshRenderer meshRenderer2;

    [SerializeField] private Material materialOff;
    [SerializeField] private Material materialOn;
    private bool isOn = false;

    public void ToggleLights()
    {
        isOn = !isOn;

        foreach (Light light in lights)
        {
            light.enabled = isOn;
        }
        switchOnVisual.SetActive(isOn); //palim i gasim dvije razlicite komponente
        switchOffVisual.SetActive(!isOn);
        if (isOn){
            meshRenderer.material = materialOn;
            meshRenderer2.material = materialOn;
            }
        else{
            meshRenderer.material = materialOff;
            meshRenderer2.material = materialOff;
        }
        Debug.Log("Lights are now " + (isOn ? "ON" : "OFF"));
    }
}
