using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the game does not start with a camera, but as soon as there is one, 
//the audio listener needs to follow it

public class AudioListenerFollow : MonoBehaviour
{
    private static AudioListenerFollow instance;

    public static AudioListenerFollow Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AudioListenerFollow>();

            return instance;
        }
    }

    public Transform playerCamera;

    // Update is called once per frame
    void Update()
    {
        if (playerCamera != null)
        {
            //transform.position = playerCamera.transform.position;
            transform.SetPositionAndRotation(playerCamera.transform.position, playerCamera.rotation);
        }
    }
}
