using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSoundPlayer : MonoBehaviour
{
    public AudioClip[] Clips;

    public Animator Animator;

    private float _lastFootstep;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!Animator) Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        var footstep = Animator.GetFloat("Footstep");
        if (Mathf.Abs(footstep) < .00001) footstep = 0f;
        if (_lastFootstep > 0 && footstep < 0 || _lastFootstep < 0 && footstep > 0)
        {
            var randomClip = Clips[Random.Range(0, Clips.Length - 1)];
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
        _lastFootstep = footstep;
    }
}
