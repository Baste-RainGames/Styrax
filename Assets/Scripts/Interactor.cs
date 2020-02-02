using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private List<IInteractable> interactables = new List<IInteractable>();

    public void InteractPressed(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        if (interactables.Count > 0)
        {
            var interactable = interactables.MinBy(DistanceToThis);

            interactable.Interact(out bool usedUp);
            if (usedUp)
                interactables.Remove(interactable);
        }
    }

    private float DistanceToThis(IInteractable arg)
    {
        var comp = (Component) arg;
        return Vector3.Distance(comp.transform.position, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var inter))
            interactables.Add(inter);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var inter))
            interactables.Remove(inter);
    }
}