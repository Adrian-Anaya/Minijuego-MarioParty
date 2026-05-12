using UnityEngine;
 
/// <summary>
/// Handles player movement, jumping, animations, dust effects, and sound.
/// Reads input in Update (jump) and FixedUpdate (horizontal movement).
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
 
    [Header("Better Jump Settings")]
    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;
 
    [Header("Visual References")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public GameObject dustLeft;
    public GameObject dustRight;
 
    [Header("Audio Settings")]
    public AudioSource jumpSound;
    public AudioSource footstepsSound;
 
    private Rigidbody2D rb2D;
 
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
 
    private void Update()
    {
        HandleJumpInput();
        UpdateJumpAnimation();
    }
 
    private void FixedUpdate()
    {
        HandleHorizontalMovement();
        ApplyBetterJumpPhysics();
    }
 
    /// <summary>
    /// Checks for jump input. Jump is only allowed when the player is grounded.
    /// </summary>
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround.isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed);
 
            if (jumpSound != null)
                jumpSound.Play();
        }
    }
 
    /// <summary>
    /// Syncs the Jump animation parameter and stops footstep sounds while airborne.
    /// </summary>
    private void UpdateJumpAnimation()
    {
        bool airborne = !CheckGround.isGrounded;
 
        animator.SetBool("Jump", airborne);
 
        if (airborne)
        {
            animator.SetBool("Run", false);
 
            if (footstepsSound != null && footstepsSound.isPlaying)
                footstepsSound.Stop();
        }
    }
 
    /// <summary>
    /// Moves the player left or right based on keyboard input.
    /// Also manages sprite flipping, dust particles, and footstep audio.
    /// </summary>
    private void HandleHorizontalMovement()
    {
        bool movingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool movingLeft  = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
 
        if (movingRight)
        {
            rb2D.linearVelocity = new Vector2(runSpeed, rb2D.linearVelocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
 
            if (CheckGround.isGrounded)
            {
                dustLeft.SetActive(true);
                dustRight.SetActive(false);
                PlayFootsteps();
            }
        }
        else if (movingLeft)
        {
            rb2D.linearVelocity = new Vector2(-runSpeed, rb2D.linearVelocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
 
            if (CheckGround.isGrounded)
            {
                dustLeft.SetActive(false);
                dustRight.SetActive(true);
                PlayFootsteps();
            }
        }
        else
        {
            // No horizontal input: stop the player and reset effects
            rb2D.linearVelocity = new Vector2(0f, rb2D.linearVelocity.y);
            animator.SetBool("Run", false);
            dustLeft.SetActive(false);
            dustRight.SetActive(false);
            StopFootsteps();
        }
    }
 
    /// <summary>
    /// Applies extra gravity when falling or when the jump button is released early,
    /// giving the player tighter control over jump height.
    /// </summary>
    private void ApplyBetterJumpPhysics()
    {
        if (!betterJump) return;
 
        // Falling: apply extra downward force for a snappier descent
        if (rb2D.linearVelocity.y < 0f)
        {
            rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        // Rising but jump button released: cut the jump short
        else if (rb2D.linearVelocity.y > 0f && !Input.GetKey(KeyCode.Space))
        {
            rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }
 
    // ── Audio helpers ─────────────────────────────────────────────────────────
 
    private void PlayFootsteps()
    {
        if (footstepsSound != null && !footstepsSound.isPlaying)
            footstepsSound.Play();
    }
 
    private void StopFootsteps()
    {
        if (footstepsSound != null && footstepsSound.isPlaying)
            footstepsSound.Stop();
    }
}