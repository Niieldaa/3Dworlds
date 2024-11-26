using System.Collections;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    public AudioSource backgroundMusic; // Original background music
    public AudioSource newMusic; // New music to play
    // public float fadeDuration = 1f; // Could not get it to work..

    private Coroutine currentFade; // Reference to ongoing fade coroutine

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone. Switching to new music.");
            if (currentFade != null) StopCoroutine(currentFade); // Stop any ongoing fade
            currentFade = StartCoroutine(SwitchMusic(backgroundMusic, newMusic));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone. Switching back to background music.");
            if (currentFade != null) StopCoroutine(currentFade); // Stop any ongoing fade
            currentFade = StartCoroutine(SwitchMusic(newMusic, backgroundMusic));
        }
    }

    private IEnumerator SwitchMusic(AudioSource from, AudioSource to)
    {
        if (from != null && to != null)
        {
            // Stop the "from" music and start the "to" music
            from.Stop();

            // Ensure the new music starts playing
            if (!to.isPlaying)
            {
                to.Play();
            }

            // You can choose to wait for the entire duration of the fade to simulate a transition, or just transition instantly
            yield return null; // No more delay, immediately switch
        }
    }
}