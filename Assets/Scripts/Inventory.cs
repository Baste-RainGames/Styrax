using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;

    public Image[] slotImages;

    private InventorySlot[] slots;

    private void Awake()
    {
        instance = this;

        slots = slotImages.Select(image => new InventorySlot(image)).ToArray();
    }

    public static bool Add(PickupContainer container)
    {
        return instance.add(container);
    }

    private bool add(PickupContainer container)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].Filled)
            {
                AudioManager.Play("Pickup");
                slots[i].Add(container);
                return true;
            }
        }

        return false;
    }

    public static bool Has(Pickup pickup)
    {
        return instance.slots.Any(s => s.pickup == pickup);
    }

    public static void Remove(Pickup pickup)
    {
        instance.slots.First(s => s.pickup == pickup).Empty();
    }
}

internal class InventorySlot
{
    private Image image;
    public Pickup pickup;

    public InventorySlot(Image image)
    {
        this.image = image;
    }

    public bool Filled => image.enabled;

    public void Add(PickupContainer container)
    {
        if (Filled)
            throw new Exception();

        this.pickup = container.pickup;
        image.enabled = true;
        image.sprite = container.pickup.sprite;
    }

    public void Empty()
    {
        if (!Filled)
            throw new Exception();

        pickup = null;
        image.enabled = false;
    }
}