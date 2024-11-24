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
        
    }

    // Method to play a random ambient sound
    public void PlayRandomAmbientSound()
    {
        
        if (ambientSounds == null || ambientSounds.Length == 0)
        {
            Debug.LogWarning("No ambient sounds assigned in the array.");
            return;
        }

        // Ensure the audio isn't already playing
        if (!audioSource.isPlaying)
        {
            // Choose a random clip from the array
            AudioClip randomClip = ambientSounds[Random.Range(0, ambientSounds.Length - 1)];
            
            // audioSource.clip = randomClip;
            // audioSource.Play();
            
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }
}