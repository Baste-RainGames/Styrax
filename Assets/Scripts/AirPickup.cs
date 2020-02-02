using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPickup : MonoBehaviour
{
    private float airAmount = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<AirTank>(out var airTank))
        {
            airTank.Fill(airAmount);
            Destroy(gameObject);
        }
    }
}
