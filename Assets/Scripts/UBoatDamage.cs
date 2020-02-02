using System.Linq;
using UnityEngine;

public class UBoatDamage : MonoBehaviour
{
    public Pickup[] requiredToFix;
    public RepairType repairType;

    private SpriteRenderer sr;

    public enum RepairType
    {
        ActivateSprite,
        DeactivateSprite
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public bool TryGetRepaired()
    {
        if (Fixed)
            return false;

        if (!CanGetRepaired)
            return false;

        foreach (var pickup in requiredToFix)
            Inventory.Remove(pickup);

        if (repairType == RepairType.ActivateSprite)
            sr.enabled = true;
        else
            sr.enabled = false;

        Fixed = true;
        return true;
    }

    public bool Fixed { get; private set; }

    public bool CanGetRepaired => requiredToFix.All(Inventory.Has);
}