using UnityEngine;

public class ReplenishRefresh : MonoBehaviour
{
    Refresh refreshRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        refreshRef = FindFirstObjectByType<Refresh>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        refreshRef.ReplenishRefresh();
        Destroy(gameObject);
    }
}
