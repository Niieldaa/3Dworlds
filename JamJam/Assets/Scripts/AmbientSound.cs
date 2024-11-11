using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] private AudioClip[] ambientSounds; // Array of ambient sounds
    [SerializeField] private float volume = 0.5f; // Volume of sounds, adjustable in inspector

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Adds AudioSource if missing
        }

        audioSource.volume = volume; // Set initial volume
        audioSource.loop = false; // Set loop to false to avoid continuous playback unless intended
    }

    // Method to play a random ambient sound
    public void PlayRandomAmbientSound()
    {
        if (ambientSounds == null || ambientSounds.Length == 0)
        {
            Debug.LogWarning("No ambient sounds assigned in the array.");
            return;
        }

        // Choose a random clip from the array
        AudioClip randomClip = ambientSounds[Random.Range(0, ambientSounds.Length)];

        if (randomClip != null)
        {
            Debug.Log("Playing ambient sound: " + randomClip.name); // Add this line
            audioSource.clip = randomClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Selected clip is null.");
        }
    }
}