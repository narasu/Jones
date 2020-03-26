using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int charges = 3;
    
    //if an enemy collides with this object, kill the enemy and reduce charge until the trap is spent
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            if (charges > 0)
            {
                other.GetComponent<Enemy>()?.Die();
                charges--;

                if (charges == 0)
                {
                    Destroy(this);
                }
            }
        }
    }
}
