using UnityEngine;
using UnityEngine.UI;

public class PressEPrompt : MonoBehaviour
{
    private static PressEPrompt instance;

    private Image image;

    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
    }

    public static void Show() => instance.image.enabled = true;
    public static void Hide() => instance.image.enabled = false;
}