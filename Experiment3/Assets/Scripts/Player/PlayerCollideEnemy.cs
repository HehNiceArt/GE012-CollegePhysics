using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollideEnemy : MonoBehaviour
{
    //particles too and sfx
    [Range(0, 60)]
    [SerializeField] int freezeFrames = 3;
    [SerializeField] bool maintainVelocity = false;
    PlayerDash playerDash;
    AudioManager audioManager;
    PlayerStun playerStun;
    Rigidbody2D rb2D;
    void Start()
    {
        playerDash = GetComponent<PlayerDash>();
        playerStun = GetComponent<PlayerStun>();
        audioManager = GetComponentInChildren<AudioManager>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyAnim enemyAnim = other.GetComponent<EnemyAnim>();
            if (playerDash.isDashing)
            {
                audioManager.PlayEliminatedSound();
                StartCoroutine(FreezeFrame());
                Debug.Log("Enemy ded");
                enemyAnim.TriggerEliminateAnimation();
            }
            else if (playerDash.isDashing == false)
            {
                Debug.Log("Player stunned");
                playerStun.StunPlayer();
            }
        }
    }
    IEnumerator FreezeFrame()
    {
        Time.timeScale = 0f;
        if (maintainVelocity)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }
        yield return new WaitForSecondsRealtime(freezeFrames / 60f);
        Time.timeScale = 1f;
    }
}
