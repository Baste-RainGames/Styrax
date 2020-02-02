using UnityEngine;

public class PickupContainer : MonoBehaviour
{
    public Pickup pickup;

    void Start()
    {
        AssignSprite();
    }

    public void AssignSprite()
    {
        if (pickup != null)
        {
            GetComponent<SpriteRenderer>().sprite = pickup.sprite;
            GetComponentInChildren<SpriteMask>().sprite = pickup.sprite;
        }
    }
}