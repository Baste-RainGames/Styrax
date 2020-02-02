using System.Collections;
using UnityEngine;

public class PufferFishExpand : MonoBehaviour
{
    //Determines how far away it'll still inflate & how long it'll stay inflated
    public float pufferFishBloatResetDelay = 5f;
    public float pufferFishRaycastRange = 10f;

    private Animator anim;
    private SpriteRenderer sr;
    private bool pufferFishResettingSize = false;

    private bool isLarge = false;
    private Coroutine deflateRoutine;

    private Collider2D damageCollider;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        damageCollider = GetComponent<Collider2D>();
        damageCollider.enabled = false;
    }

    //Controls where the raycast line is projected, based on wether the sprite is flipped or not
    void FixedUpdate()
    {
        Vector2 rayDir;
        if (sr.flipX)
            rayDir = Vector2.left;
        else
            rayDir = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, pufferFishRaycastRange);
        if (hit.collider != null && !isLarge)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Inflate());

                if (pufferFishResettingSize)
                {
                    StopCoroutine(deflateRoutine);
                }
            }
        }
        else if (hit.collider == null && isLarge && !pufferFishResettingSize)
        {
            deflateRoutine = StartCoroutine(Deflate());
        }
    }

    IEnumerator Inflate()
    {
        isLarge = true;
        anim.Play("Puffer Inflate");
        AudioManager.Play("Inflate");
        yield return new WaitForSeconds(0.48f);
        damageCollider.enabled = true;
        StartCoroutine(Deflate());
    }

    IEnumerator Deflate()
    {
        pufferFishResettingSize = true;
        yield return new WaitForSeconds(pufferFishBloatResetDelay);
        anim.Play("Puffer Deflate");
        AudioManager.Play("Deflate");
        yield return new WaitForSeconds(0.42f);
        damageCollider.enabled = false;
        isLarge = false;
        pufferFishResettingSize = false;
    }
}