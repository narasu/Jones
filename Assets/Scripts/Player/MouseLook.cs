using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] private Transform playerBody;
    [SerializeField] private string mouseXInputName, mouseYInputName;

    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float rotationSpeed = 5f;

    private float xAxisClamp;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Awake()
    {
        LockCursor();
    }

    private void Update()
    {
        CameraRotation();
    }
    
    private void LockCursor()
    {
        //hides and locks cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void CameraRotation()
    {
        //set mouse movement values
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;
        
        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
