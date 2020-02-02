using UnityEngine;

public class SpriteFromXVel : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (rb == null || sr == null)
        {
            Debug.LogError("PANIC!");
            Destroy(this);
        }
    }

    private void Update()
    {
        sr.flipX = rb.velocity.x < 0;
    }
}