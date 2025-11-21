using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpStrength = 12f;
    public float gravityUp = 1.5f;
    public float gravityDown = 3f;
    public GameObject groundDetector;
    public GameObject leftWallDetector;
    public GameObject rightWallDetector;
    public LayerMask groundLayer;
    public Sprite rightSprite;
    public Sprite leftSprite;
    public Sprite ufoSprite;
    private Sprite normalSprite;
    public InputAction horizontalInput;
    public InputAction jumpInput;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    bool grounded;
    bool touchingLeftWall;
    bool touchingRightWall;
    bool jumpAvailable = true;
    public float wallSlideSpeed = -2f;
    public float coyoteTime = 0.15f;
    float coyoteTimer;
    bool isUFO = false;
    float ufoTimer;
    public float ufoSpeed = 6f;
    private float originalSpeed;
    private float originalJumpForce;
    public bool isInWater = false;
    public float swimUpSpeed = 5f;
    public AudioClip ufoSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        horizontalInput.Enable();
        jumpInput.Enable();
        normalSprite = sr.sprite;
        originalSpeed = speed;
        originalJumpForce = jumpStrength;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (isUFO)
        {
            HandleUFOControls();
            ufoTimer -= Time.deltaTime;
            if (ufoTimer <= 0f)
            {
                DeactivateUFOForm();
            }
            return;
        }
        grounded = Physics2D.OverlapCircle(groundDetector.transform.position, 0.2f, groundLayer);
        touchingLeftWall = Physics2D.OverlapCircle(leftWallDetector.transform.position, 0.1f, groundLayer);
        touchingRightWall = Physics2D.OverlapCircle(rightWallDetector.transform.position, 0.1f, groundLayer);

        float direction = horizontalInput.ReadValue<float>();
        bool isWalking = Mathf.Abs(direction) > 0.1f && grounded;
        bool isJumping = !grounded && rb.linearVelocity.y > 0.1f;
        bool isFalling = !grounded && rb.linearVelocity.y < -0.1f;

        float airFactor = grounded ? 1f : 0.6f;
        rb.linearVelocity = new Vector2(direction * speed * airFactor, Mathf.Max(rb.linearVelocity.y, -3f));
        rb.gravityScale = grounded ? 1 : rb.linearVelocity.y > 0 ? gravityUp : gravityDown;

        if (grounded)
        {
            coyoteTimer = coyoteTime;
            jumpAvailable = true;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (jumpInput.WasPerformedThisFrame() && jumpAvailable && coyoteTimer > 0f)
        {
            var audioSource = GetComponent<AudioSource>();

            audioSource.Play();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            jumpAvailable = false;
            coyoteTimer = 0f;
        }

        if (!grounded)
        {
            if (isJumping)
            {
                if (touchingLeftWall && direction < -0.1f)
                {
                    rb.linearVelocity = new Vector2(Mathf.Max(rb.linearVelocity.x, 0f), rb.linearVelocity.y);
                }
                else if (touchingRightWall && direction > 0.1f)
                {
                    rb.linearVelocity = new Vector2(Mathf.Min(rb.linearVelocity.x, 0f), rb.linearVelocity.y);
                }
            }

            if (isFalling && ((touchingLeftWall && direction < -0.1f) || (touchingRightWall && direction > 0.1f)))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, wallSlideSpeed);
            }
        }

        if (!isUFO)
        {
            if (direction > 0.1f) sr.sprite = rightSprite;
            else if (direction < -0.1f) sr.sprite = leftSprite;
        }

        if (isInWater)
        {
            rb.gravityScale = 0.3f;

            if (jumpInput.IsPressed())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, swimUpSpeed);
            }
        }

        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsJumping", isJumping);
        anim.SetBool("IsGrounded", grounded);
        anim.SetBool("IsFalling", isFalling);
        anim.SetBool("IsTouchingWall", (touchingLeftWall || touchingRightWall));
        anim.SetFloat("MoveX", direction);
    }
    void HandleUFOControls()
    {
        float x = horizontalInput.ReadValue<float>();
        float y = 0f;

        if (Keyboard.current.wKey.isPressed) y = 1f;
        else if (Keyboard.current.sKey.isPressed) y = -1f;

        Vector2 move = new Vector2(x, y).normalized;
        rb.linearVelocity = move * ufoSpeed;
    }
    public void ActivateUFOForm(float duration)
    {
        isUFO = true;
        ufoTimer = duration;
        rb.gravityScale = 0f;
        sr.sprite = ufoSprite;
        anim.enabled = false;
        if (ufoSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(ufoSound);
        }
    }

    public void DeactivateUFOForm()
    {
        StopAllCoroutines();
        isUFO = false;
        ufoTimer = 0f;
        rb.gravityScale = 1f;
        rb.linearVelocity = Vector2.zero;
        sr.sprite = normalSprite;
        anim.enabled = true;
    }
    public void ResetMovementStats()
    {
        speed = originalSpeed;
        jumpStrength = originalJumpForce;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
            isInWater = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
            isInWater = false;
    }
}