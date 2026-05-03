using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int speed = 10;
    [SerializeField] int jumpForce = 10;
    [SerializeField] int jumpCount = 1;
    int maxJumpCount;

    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter = 0f;

    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;

    public bool isJumping;
    public Transform GroundCheckPos;
    public Vector2 GroundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask GroundLayer;

    public int RefreshCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxJumpCount = jumpCount;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.velocity = Vector2.up * jumpForce;
        //}

        //Debug.Log(rb.linearVelocity);

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            //isJumping = false;
        }
        else if (!isGrounded())
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
            //isJumping = true;
            //jumpCount--;
            if (coyoteTimeCounter < 0 & jumpCount > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount = 0;
            }
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
            //isJumping = false;
        }

        //if (jumpBufferCounter > 0f & coyoteTimeCounter > 0f & !isJumping)

        //if (jumpBufferCounter > 0f & coyoteTimeCounter > 0f & !isJumping)
        //{
        //    if (isJumping)
        //    //isJumping = true;
        //    Debug.Log("jump");
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //    isJumping = true;

        //    jumpBufferCounter = 0f;
        //    coyoteTimeCounter = 0f;
        //}
        //else if (jumpBufferCounter > 0f & coyoteTimeCounter > 0f & isJumping & jumpCount > 0)
        //{
        //    Debug.Log("double jump");
        //    jumpCount--;
        //    isJumping = false;
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //}

        if (jumpBufferCounter > 0f) //could be better, if you have time come revisit //moving while jumping increases jumpheight
        {
            if (coyoteTimeCounter > 0f && !isJumping)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isJumping = true;

                jumpBufferCounter = 0f;
                coyoteTimeCounter = 0f;
            }
            else if (isJumping)
            {
                if (jumpCount > 0)
                {
                    jumpCount--;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    jumpBufferCounter = 0f;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        //float targetSpeed = horizontalInput * speed;

        //var runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        //var runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        //targetSpeed = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, 1);
        //float speedDif = targetSpeed - rb.linearVelocity.x;
        //float movement = speedDif * accelRate;
        //rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        //transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * 1.25f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(GroundCheckPos.position, GroundCheckSize, 0, GroundLayer) & rb.linearVelocity.y <= 0) //kinda ducttape rn
        {
            jumpCount = maxJumpCount;
            isJumping = false;
            return true;
        }
        return false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isGrounded()) //stopping effector from applying force when jumping
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(GroundCheckPos.position, GroundCheckSize);
    }
}
