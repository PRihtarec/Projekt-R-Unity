using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    GamesController gamesController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gamesController = FindObjectOfType<GamesController>();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("mouseSensitivity"))
        {
            sensX=PlayerPrefs.GetFloat("mouseSensitivity")+0.5f;
            sensY=PlayerPrefs.GetFloat("mouseSensitivity")+0.5f;
        }
        else
        {
            sensX=1f;
            sensX=1f;
        }
        if (!gamesController.isMinigameInProgress()){
        // get mouse input
        float mouseX = Input.GetAxis("Mouse X") *  sensX;
        float mouseY = Input.GetAxis("Mouse Y") *  sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}