using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 12f;

    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Wall Jump")]
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallJumpX = 8f;
    [SerializeField] private float wallJumpY = 12f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;

    private float horizontalInput;
    private bool facingRight = true;

    private float wallJumpCooldown = 0.2f;
    private float wallJumpCounter;

    // MOBILE BUTTON
    private bool jumpPressed;

    private void Awake()
    {
        // Ambil component
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // GERAK PLAYER
        body.velocity = new Vector2(
            horizontalInput * speed,
            body.velocity.y
        );

        // FLIP KARAKTER
        Flip();

        // ANIMATOR
        if (anim != null)
        {
            anim.SetBool("run", horizontalInput != 0);
            anim.SetBool("grounded", isGrounded());
        }

        // WALL SLIDE
        WallSlide();

        // JUMP BUTTON
        if (jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }

        // COOLDOWN WALL JUMP
        if (wallJumpCounter > 0)
        {
            wallJumpCounter -= Time.deltaTime;
        }
    }

    // =========================
    // BUTTON MOBILE
    // =========================

    // GERAK KIRI
    public void MoveLeft()
    {
        horizontalInput = -1;
    }

    // GERAK KANAN
    public void MoveRight()
    {
        horizontalInput = 1;
    }

    // BERHENTI
    public void StopMove()
    {
        horizontalInput = 0;
    }

    // TOMBOL JUMP
    public void PressJump()
    {
        jumpPressed = true;
    }

    // =========================
    // JUMP
    // =========================

    private void Jump()
    {
        // JUMP NORMAL
        if (isGrounded())
        {
            body.velocity = new Vector2(
                body.velocity.x,
                jumpPower
            );

            // SOUND
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

            // ANIMASI
            if (anim != null)
                anim.SetTrigger("Jump");
        }

        // WALL JUMP
        else if (onWall() && !isGrounded())
        {
            float jumpDirection = facingRight ? -1 : 1;

            body.velocity = new Vector2(
                jumpDirection * wallJumpX,
                wallJumpY
            );

            wallJumpCounter = wallJumpCooldown;

            // SOUND
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

            // FLIP OTOMATIS
            facingRight = !facingRight;

            // ANIMASI
            if (anim != null)
                anim.SetTrigger("Jump");
        }
    }

    // =========================
    // WALL SLIDE
    // =========================

    private void WallSlide()
    {
        if (onWall() && !isGrounded() && horizontalInput != 0)
        {
            body.velocity = new Vector2(
                body.velocity.x,
                -wallSlideSpeed
            );
        }
    }

    // =========================
    // FLIP KARAKTER
    // =========================

    private void Flip()
    {
        // HADAP KANAN
        if (horizontalInput > 0.01f)
        {
            sprite.flipX = false;
            facingRight = true;
        }

        // HADAP KIRI
        else if (horizontalInput < -0.01f)
        {
            sprite.flipX = true;
            facingRight = false;
        }

        // UKURAN PLAYER TETAP
        transform.localScale =
            new Vector3(0.5f, 0.5f, 0.5f);
    }

    // =========================
    // CEK GROUND
    // =========================

    private bool isGrounded()
    {
        RaycastHit2D raycastHit =
            Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0,
                Vector2.down,
                0.1f,
                groundLayer
            );

        return raycastHit.collider != null;
    }

    // =========================
    // CEK WALL
    // =========================

    private bool onWall()
    {
        Vector2 direction =
            facingRight ? Vector2.right : Vector2.left;

        RaycastHit2D raycastHit =
            Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0,
                direction,
                0.1f,
                wallLayer
            );

        return raycastHit.collider != null;
    }

    // =========================
    // GIZMOS
    // =========================

    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(
            boxCollider.bounds.center + Vector3.down * 0.1f,
            boxCollider.bounds.size
        );
    }
}