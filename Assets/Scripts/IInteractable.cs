public interface IInteractable
{
    void Interact(out bool usedUp);
    bool CanInteract { get; }
}