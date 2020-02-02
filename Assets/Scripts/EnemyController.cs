using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Determines the edges of enemy patrol area
    public Vector2 start = new Vector2(-8, 0);
    public Vector2 end = new Vector2(8, 0);

    //Determines how fast the enemy moves
    public float enemyMovementSpeed = 0.35f;

    public bool flipScaleInsteadOfSprite;
    public bool defaultIsFlippedX;

    private SpriteRenderer sr;

    private bool increasing = true;
    private float movement_t;

    private Vector2 startPos;
    [Range(0f, 1f)]
    private float startOffset = .5f;

    [NonSerialized] public bool stopMoving;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        startPos = transform.position;
        movement_t = startOffset;
    }

    void Update()
    {
        if (stopMoving)
            return;

        if (increasing)
        {
            movement_t += Time.deltaTime * enemyMovementSpeed;
            if (movement_t > 1)
            {
                increasing = false;
                movement_t = 1;
            }
        }
        else
        {
            movement_t -= Time.deltaTime * enemyMovementSpeed;
            if (movement_t < 0)
            {
                increasing = true;
                movement_t = 0;
            }
        }

        var newPos = Vector2.Lerp(startPos + start, startPos + end, Mathf.SmoothStep(0f, 1f, movement_t));

        var flipX = newPos.x < transform.position.x;
        if (defaultIsFlippedX)
        {
            flipX = !flipX;
        }

        if (flipScaleInsteadOfSprite)
        {
            transform.localScale = new Vector3(flipX ? -1f : 1f, 1f, 1f);
        }
        else
        {
            sr.flipX = flipX;
        }

        transform.position = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        var defaultPos = Application.isPlaying ? startPos : (Vector2) transform.position;
        var endPoint   = defaultPos + end;
        var startPoint = defaultPos + start;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPoint, .25f);
        Gizmos.DrawSphere(endPoint, .25f);
        Gizmos.DrawLine(startPoint, endPoint);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Vector3.Lerp(startPoint, endPoint, startOffset), .25f);
    }
}

