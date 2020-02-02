using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public Sprite openSprite;
    public GameObject contentsPrefab;
    public Vector2 spawnOffset;
    public Vector2 spawnStartVelocity;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(out bool usedUp)
    {
        sr.sprite = openSprite;
        AudioManager.Play("Open Chest");
        usedUp = true;
        if (contentsPrefab != null)
        {
            var spawned = Instantiate(contentsPrefab, transform.position + (Vector3) spawnOffset, Quaternion.identity);
            if (spawned.TryGetComponent<Rigidbody2D>(out var rb2d))
                rb2d.velocity = spawnStartVelocity;
        }
        Destroy(this);
    }
}