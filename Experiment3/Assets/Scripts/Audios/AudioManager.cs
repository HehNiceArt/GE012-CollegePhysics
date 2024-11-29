using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audios;
    PlayerController playerController;
    PlayerDash playerDash;
    PlayerStun playerStun;
    private bool wasJumping = false;
    private bool wasDashing = false;
    private bool wasStunned = false;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerDash = FindObjectOfType<PlayerDash>();
        playerStun = FindObjectOfType<PlayerStun>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (playerController != null && playerController.isJumping && !wasJumping)
        {
            PlayJumpSound();
        }
        wasJumping = playerController.isJumping;

        if (playerDash != null && playerDash.isDashing && !wasDashing)
        {
            PlayDashSound();
        }
        wasDashing = playerDash.isDashing;

        if (playerStun != null && playerStun.isStunned && !wasStunned)
        {
            PlayStunSound();
        }
        wasStunned = playerStun.isStunned;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PEShirt"))
        {
            PlayWinSound();
        }
    }

    public void PlayJumpSound()
    {
        PlayAudio("SFX_Jump");
    }

    public void PlayDashSound()
    {
        PlayAudio("SFX_Dash");
    }

    public void PlayStunSound()
    {
        PlayAudio("SFX_Stun2");
    }

    public void PlayWinSound()
    {
        PlayAudio("SFX_Win");
    }
    public void PlayEliminatedSound()
    {
        PlayAudio("SFX_Eliminated");
    }

    private void PlayAudio(string audioName)
    {
        AudioClip clip = FindAudioByName(audioName);
        if (clip == null)
        {
            Debug.Log("Missing Audio Clip");
            return;
        }
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();
    }

    private AudioClip FindAudioByName(string audioName)
    {
        foreach (AudioClip audio in audios)
        {
            if (audio.name == audioName)
            {
                return audio;
            }
        }
        return null;
    }
}
