using UnityEditor;

[CustomEditor(typeof(PickupContainer)), CanEditMultipleObjects]
public class PickupContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        foreach (PickupContainer pc in targets)
        {
            var before = pc.pickup;
            base.OnInspectorGUI();
            if (pc.pickup != before)
                pc.AssignSprite();
        }

    }
}