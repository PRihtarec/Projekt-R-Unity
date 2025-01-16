using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private Light[] lights; //svjetla koja prekidač kontrolira
    [SerializeField] private GameObject switchOnVisual;
    [SerializeField] private GameObject switchOffVisual;
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
        Debug.Log("Lights are now " + (isOn ? "ON" : "OFF"));
    }
}
