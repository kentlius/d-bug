using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float maxspeed = 6f;
    private float jumpingPower = 8f;
    private float newX;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public GameObject gameOverScreen;
    public SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    private bool doubleJumped = false;

    void Update()
    {
        if ((rb.velocity.x > 0 && !facingRight) || (rb.velocity.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        if ((Input.GetKey("a") || Input.GetKey("d"))&& (!Input.GetKey(KeyCode.Mouse0)))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            newX = rb.velocity.x + horizontal*0.15f;
            if ((horizontal > 0  && rb.velocity.x < 0) || (horizontal < 0 && rb.velocity.x > 0))
            {
                newX = rb.velocity.x + horizontal*0.05f;
                rb.velocity = new Vector2(newX, rb.velocity.y);
            }
            if (newX < maxspeed && newX > -maxspeed){
                rb.velocity = new Vector2(newX, rb.velocity.y);
            }
            // if (IsGrounded())
            // {
            //     newX = rb.velocity.x + horizontal*0.5f;
            //     rb.velocity = new Vector2(newX, rb.velocity.y);
            // }
            // else
            // {
            //     if (Input.GetKey("d")){
            //         if (rb.velocity.x < horizontal * speed && rb.velocity.x >= 0)
            //         {
            //             newX = rb.velocity.x + horizontal*0.5f;
            //             rb.velocity = new Vector2(newX, rb.velocity.y);
            //         }
            //         else if (rb.velocity.x < 0)
            //         {
            //             newX = rb.velocity.x + horizontal*0.5f;
            //             rb.velocity = new Vector2(newX, rb.velocity.y);
            //         }
            //     } 
            //     if (Input.GetKey("a")){
            //         if (rb.velocity.x > horizontal * speed && rb.velocity.x <= 0)
            //         {
            //             newX = rb.velocity.x + horizontal*0.5f;
            //             rb.velocity = new Vector2(newX, rb.velocity.y);
            //         } else if (rb.velocity.x > 0)
            //         {
            //             newX = rb.velocity.x + horizontal*0.5f;
            //             rb.velocity = new Vector2(newX, rb.velocity.y);
            //         }
            //     }
                
            // }
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && !IsGrounded() && !doubleJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJumped = true;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (IsGrounded())
        {
            doubleJumped = false;
        }
    
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameOverScreen.SetActive(true);
            //set time to 0
            Time.timeScale = 0;
        }
    }
}