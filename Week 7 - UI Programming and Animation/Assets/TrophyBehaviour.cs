using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyBehaviour : MonoBehaviour
{
    AudioSource audioData;
    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioData.Play();
        }
    }
}
