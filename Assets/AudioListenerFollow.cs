using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            transform.position = playerCamera.transform.position;
        }
    }
}
