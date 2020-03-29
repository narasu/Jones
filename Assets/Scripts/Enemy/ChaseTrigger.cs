using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Enemy enemy;
#pragma warning restore 0649

    //make the enemy chase the player when the player collides with the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.target = other.transform;
            enemy.MakeNoise(enemy.growl);
            enemy.ChasePlayer();
            gameObject.SetActive(false);
        }
    }
}
