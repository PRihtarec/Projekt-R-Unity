using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform cameraRotation;

    private void Update()
    {
        transform.rotation = cameraRotation.rotation;
    }
}
