using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private readonly float speed = 6f;
    private readonly float jumpingPower = 8f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask bounceLayer;
    public SpriteRenderer spriteRenderer;
    public bool doubleJumped = false;
    private float newX;
    public Animator anim;

    [Header("Collision")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;

    [Header("Better Jumping")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    void Update()
    {
        anim.SetBool("grounded", IsGrounded());
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));

        horizontal = Input.GetAxisRaw("Horizontal");
        if (!Input.GetKey(KeyCode.Mouse0) && !IsGrounded())
        {
            newX = rb.velocity.x + horizontal * 0.15f;
            if ((horizontal > 0 && rb.velocity.x < 0) || (horizontal < 0 && rb.velocity.x > 0))
            {
                newX = rb.velocity.x + horizontal * 0.05f;
                rb.velocity = new Vector2(newX, rb.velocity.y);
            }
            if (newX < speed && newX > -speed)
            {
                rb.velocity = new Vector2(newX, rb.velocity.y);
            }
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }

        if (Input.GetButtonDown("Jump") && !IsGrounded() && !doubleJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJumped = true;
        }

        if (IsGrounded())
        {
            doubleJumped = false;
        }

        if (Bounce())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower*2);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Mouse0) && IsGrounded())
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
    }


    private bool Bounce()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, bounceLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
            // Vector3 localScale = transform.localScale;
            // localScale.x *= -1f;
            // transform.localScale = localScale;
        }
    }
}