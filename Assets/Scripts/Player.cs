using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int speed = 10;
    [SerializeField] int jumpForce = 10;
    [SerializeField] int jumpCount = 2;
    int maxJumpCount;
    bool onGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxJumpCount = jumpCount;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space) & onGround)
        {
            Vector2 currentVelocity = rb.velocity;
            rb.velocity = new Vector2(currentVelocity.x, jumpForce);
            jumpCount--;
        }

        if (jumpCount <= 0)
        {
            jumpCount = 0;
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = maxJumpCount;
            onGround = true;
        }
    }
}
