using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Dominic Quirante
[RequireComponent(typeof(Rigidbody2D), typeof(PlayerWallRide), typeof(PlayerDash))]
[RequireComponent(typeof(PlayerStun), typeof(PlayerCollideEnemy))]
public class PlayerController : MonoBehaviour
{
    [Header("SPEED")]
    [SerializeField] float aceleration = 5;
    [SerializeField] float decceleration = 40;
    [SerializeField] float maxSpeed = 13;

    [Space(10f)]
    [Header("JUMP")]
    [SerializeField] float jumpHeight = 10;
    [Range(1f, 2f)]
    [SerializeField] float jumpPeakLength = 1;
    [Range(0f, 100f)]
    [SerializeField, Tooltip("In Percentage %")] float jumpMoveModifier = 90;
    [Range(0f, 1f)]
    [SerializeField] float jumpCooldown = 0.4f;
    [Range(0f, 1f)]
    [SerializeField] float jumpInputBuffer = 0.2f;
    float jumpCooldownTimer = 0f;

    [Space(10f)]
    [Header("GRAVITY")]
    [SerializeField] float gravityMultiplier = 1;
    [SerializeField, Tooltip("In Percentage %")] float highGravityModifier = 175;
    [SerializeField] float gravity = 6;
    [Space(10f)]
    [Header("ANIMATION")]
    [SerializeField] Animator playerAnim;

    [Space(10f)]
    [Header("BOOLEANS")]
    public bool isJumping = false;
    public bool isOnGround = true;
    public bool facingLeft = true;
    public SpeedrunTimer speedrunTimer;
    [HideInInspector] public Rigidbody2D rb;
    float currentGravity;
    float move;
    float initialVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        speedrunTimer.StartTimer();
    }
    private void Update()
    {
        if (GetComponent<PlayerStun>().isStunned)
        {
            return;
        }
        currentGravity = gravity * gravityMultiplier;
        rb.gravityScale = gravityMultiplier * gravity;
        move = Input.GetAxisRaw("Horizontal");

        jumpCooldownTimer -= Time.deltaTime;
        JumpMovement();
        AnimationParameters();
    }
    private void FixedUpdate()
    {
        HorizontalMovement();
        CapMovementSpeed();
    }
    float Movement()
    {
        if (isJumping)
        {
            float inAirMovement = rb.velocity.x * (jumpMoveModifier * 0.01f);
            return inAirMovement;
        }
        else
        {
            return rb.velocity.x;
        }
    }
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void HorizontalMovement()
    {
        if (move > 0)
        {
            playerAnim.SetBool("isRunning", true);
            if (facingLeft)
            {
                Flip();
                facingLeft = false;
            }

            if (rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
            float righInput = Movement() + (maxSpeed * aceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(righInput, rb.velocity.y);
        }
        else if (move < 0)
        {
            playerAnim.SetBool("isRunning", true);
            if (!facingLeft)
            {
                Flip();
                facingLeft = true;
            }

            if (rb.velocity.x > 0) rb.velocity = new Vector2(0, rb.velocity.y);
            float leftInput = Movement() + (-maxSpeed * aceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(leftInput, rb.velocity.y);
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                float deccelerationAmount = decceleration * Time.fixedDeltaTime;
                if (rb.velocity.x > 0)
                {
                    rb.velocity = new Vector2(Mathf.Max(Movement() - deccelerationAmount, 0), rb.velocity.y);
                }
                else if (rb.velocity.x < 0)
                {
                    rb.velocity = new Vector2(Mathf.Min(Movement() + deccelerationAmount, 0), rb.velocity.y);
                }
                playerAnim.SetBool("isRunning", false);
            }
        }
    }
    void CapMovementSpeed()
    {
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
    }
    void JumpMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCooldownTimer <= 0 && isOnGround)
        {
            isJumping = true;
            isOnGround = false;
            rb.velocity = new Vector2(Movement(), Jump());
            jumpCooldownTimer = jumpCooldown;
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpInputBuffer);
        }
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = currentGravity;
        }
        else if (rb.velocity.y < initialVelocity)
        {
            float highGravity = currentGravity * (highGravityModifier * 0.01f);
            rb.gravityScale = highGravity;
        }
        if (rb.velocity.y == 0)
        {
            rb.gravityScale = currentGravity;
        }
    }
    void AnimationParameters()
    {
        if (isJumping)
        {
            playerAnim.SetBool("isJumping", true);
        }
        else
        {
            playerAnim.SetBool("isJumping", false);
        }
    }
    public float Jump()
    {
        initialVelocity = 2 * jumpHeight / jumpPeakLength;
        return Mathf.Abs(initialVelocity);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isJumping = false;
        }
    }
}