using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopsPunch : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Play the audio when colliding with an object with the "Enemy" tag
            if (audioSource != null)
            {
                audioSource.Play();
                
            }
        }
    }
}

