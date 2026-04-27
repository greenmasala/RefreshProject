using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int speed = 10;
    [SerializeField] int jumpForce = 10;
    [SerializeField] int jumpCount = 2;
    int maxJumpCount;

    public Transform GroundCheckPos;
    public Vector2 GroundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask GroundLayer;

    public float BaseGravity = 2f;
    public float MaxFallSpeed = 18f;
    public float FallSpeedMultiplier = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxJumpCount = jumpCount;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (onGround && !Input.GetKeyDown(KeyCode.Space))
        //{
        //    doubleJump = false;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (onGround || doubleJump)
        //    {
        //        var currentVelocity = rb.linearVelocity;
        //        rb.linearVelocity = new Vector2(currentVelocity.x, jumpForce);
        //        jumpCount--;

        //        doubleJump = !doubleJump;
        //    }
        //}

        if (isGrounded() || jumpCount != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount--;
            }
        }

        //if (!isGrounded() & jumpCount == maxJumpCount)
        //{
        //    jumpCount = 1;
        //}
    }
    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
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
        if (Physics2D.OverlapBox(GroundCheckPos.position, GroundCheckSize, 0, GroundLayer))
        {
            jumpCount = maxJumpCount;
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
