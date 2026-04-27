using UnityEngine;

public class Jumppad : MonoBehaviour
{
    public int JumpForce = 15;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
}
