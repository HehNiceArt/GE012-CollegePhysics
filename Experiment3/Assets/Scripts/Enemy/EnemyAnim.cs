using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator animator;
    private AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void TriggerEliminateAnimation()
    {
        animator.SetTrigger("isDashed");

    }

    public void EliminateEnemy()
    {
        Destroy(gameObject);
        //audioManager.PlayEliminatedSound();
    }
}
