using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float FallDelay = 1f;
    public float DestroyDelay = 5f;
    float delaySequence;

    Rigidbody2D rb;
    SpriteRenderer color;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        color = rb.GetComponent<SpriteRenderer>();
        delaySequence = FallDelay / 4;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(FallDelay);
        color.color = Color.indianRed;
        yield return new WaitForSeconds(FallDelay);
        color.color = Color.orangeRed;
        yield return new WaitForSeconds(FallDelay);
        color.color = Color.red;
        yield return new WaitForSeconds(FallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, DestroyDelay);
    }
}
