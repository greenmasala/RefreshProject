using System.Collections;
using UnityEngine;

public class RefreshPlatforms : MonoBehaviour
{
    Refresh refreshRef;
    public Transform[] MovePoints;
    public int MoveSpeed = 5;
    //Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        refreshRef = FindFirstObjectByType<Refresh>();
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (refreshRef.HasRefreshed)
        {
            if (transform.position != MovePoints[1].position)
            {
                //rb.MovePosition(MovePoints[1].position * MoveSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, MovePoints[1].position, MoveSpeed * Time.deltaTime);
            }
        }

        else
        {
            if (transform.position != MovePoints[0].position)
            {
                //rb.MovePosition(MovePoints[0].position * MoveSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, MovePoints[0].position, MoveSpeed * Time.deltaTime);
                Debug.Log("running");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}

