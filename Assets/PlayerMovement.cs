using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float maxspeed = 12f;
    private float jumpingPower = 8f;
    private float newX;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        if ((Input.GetKey("a") || Input.GetKey("d"))&& (!Input.GetKey(KeyCode.Mouse0)))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            newX = rb.velocity.x + horizontal*0.3f;
            if ((horizontal > 0  && rb.velocity.x < 0) || (horizontal < 0 && rb.velocity.x > 0))
            {
                newX = rb.velocity.x + horizontal*0.15f;
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

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

}