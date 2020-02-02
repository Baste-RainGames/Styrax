using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirTankUI : MonoBehaviour
{
    public Image fillBar;
    public Image head;
    public Image[] holeImages;
    public Image[] fixImages;

    public float minHeadPos;
    public float maxHeadPos;

    public Sprite fill0;
    public Sprite fill10;
    public Sprite fill20;
    public Sprite fill50;
    public Sprite fill80;
    public Sprite fill90;

    private List<(float weight, Sprite sprite)> spriteWeights;
    private int maxHoles;

    private void Awake()
    {
        spriteWeights = new List<(float, Sprite)>
        {
            (0f, fill0),
            (.1f, fill10),
            (.2f, fill20),
            (.5f, fill50),
            (.8f, fill80),
            (.9f, fill90),
        };

        SetHoles(0);
    }

    public void SetAir(float air)
    {
        var fillAmount = Mathf.InverseLerp(0f, Settings.AirTankMaxFill, air);
        fillBar.fillAmount = fillAmount;

        var yPos = Mathf.Lerp(minHeadPos, maxHeadPos, fillAmount);
        head.rectTransform.anchoredPosition = new Vector2(0f, yPos);

        var (_, sprite) = spriteWeights.MinBy(spriteWeight => Mathf.Abs(spriteWeight.weight - fillAmount));

        head.sprite = sprite;
    }

    public void SetHoles(int holes)
    {
        maxHoles = Mathf.Max(holes, maxHoles);

        int i = 0;
        for (; i < holes; i++)
        {
            holeImages[i].enabled = true;
            fixImages[i].enabled = false;
        }

        for (; i < maxHoles; i++)
        {
            holeImages[i].enabled = false;
            fixImages[i].enabled = true;
        }

        for (; i < holeImages.Length; i++)
        {
            holeImages[i].enabled = false;
            fixImages[i].enabled = false;
        }
    }
}
