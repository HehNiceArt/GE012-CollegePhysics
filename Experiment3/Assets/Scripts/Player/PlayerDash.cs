using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashForce = 24f;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCooldown;
    [SerializeField] bool canDash = true;
    public bool isDashing;
    Animator playerAnim;

    PlayerController playerController;
    Rigidbody2D rb2D;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAnim = GetComponent<Animator>();
        rb2D = playerController.rb;
    }
    void Update()
    {
        if (GetComponent<PlayerStun>().isStunned)
        {
            return;
        }
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            playerAnim.SetBool("isDashing", true);
        }
        else
        {
            playerAnim.SetBool("isDashing", false);
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(rb2D.velocity.x, dashForce);
        isDashing = true;
        yield return new WaitForSeconds(dashTime);
        rb2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
