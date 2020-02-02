using System.Linq;
using UnityEngine;

public class UBoat : MonoBehaviour, IInteractable
{

    public Sprite fixedSprite;

    private SpriteRenderer sr;
    private UBoatDamage[] damages;
    private bool isFixed;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        damages = GetComponentsInChildren<UBoatDamage>();
    }

    public void Interact(out bool usedUp)
    {
        if (isFixed)
        {
            InGameUI.ShowGameOverScreen();
            usedUp = false;
            return;
        }

        foreach (var damage in damages)
        {
            if (damage.TryGetRepaired())
                break;
        }

        var anyNotFixed = false;
        foreach (var damage in damages)
        {
            if (!damage.Fixed)
                anyNotFixed = true;
        }

        if (!anyNotFixed)
        {
            sr.sprite = fixedSprite;
            isFixed = true;
            foreach (var childSR in gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                if (childSR != sr)
                    childSR.enabled = false;
            }
        }

        usedUp = false;
    }

    public bool CanInteract
    {
        get
        {
            if (isFixed)
                return true;

            return damages.Any(d => d.CanGetRepaired);
        }
    }
}