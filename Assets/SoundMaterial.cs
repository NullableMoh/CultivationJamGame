using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaterial : MonoBehaviour
{
    [SerializeField] AudioClip sound;

    bool canPlay;

    AudioSource audioSource;
    PlayerMovement playerMovement;
    
    private void OnEnable()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.OnPlayerJump += PlayJumpSound;
        playerMovement.OnPlayerMove += PlayMoveSound;
        playerMovement.OnPlayerNotMoving += StopMoveSound;
    }

    private void OnDisable()
    {
        playerMovement.OnPlayerJump -= PlayJumpSound;
        playerMovement.OnPlayerMove -= PlayMoveSound;
        playerMovement.OnPlayerNotMoving -= StopMoveSound;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        canPlay = false;
    }


    void PlayJumpSound()
    {
        if(canPlay)
        {
            PlaySound(sound);
        }
    }

    void PlayMoveSound()
    {
        if (canPlay)
        {
            PlaySound(sound);
        }
    }

    void StopMoveSound()
    {
        audioSource.Stop();
    }

    void PlaySound(AudioClip clip)
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            canPlay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            canPlay = false;
        }
    }


}
