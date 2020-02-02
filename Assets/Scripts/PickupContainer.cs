using UnityEngine;

public class PickupContainer : MonoBehaviour
{
    public Pickup pickup;
    private SpriteRenderer sr;

    void Start()
    {
        AssignSprite();
    }

    public void AssignSprite()
    {
        if (pickup != null)
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = pickup.sprite;
        }
    }
}