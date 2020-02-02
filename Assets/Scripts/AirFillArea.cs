using UnityEngine;

public class AirFillArea : MonoBehaviour
{
    public float airFillRate = 1f;

    public float GetAir()
    {
        return Time.deltaTime * airFillRate;
    }
}
