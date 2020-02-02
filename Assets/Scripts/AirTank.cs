using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirTank : MonoBehaviour
{
    private float _air;
    private float Air
    {
        get => _air;
        set
        {
            _air = Mathf.Clamp(value, 0f, Settings.AirTankMaxFill);
            _ui.SetAir(_air);
            rb2d.gravityScale = Mathf.Lerp(Settings.PlayerGravityNoAir, Settings.PlayerGravityMaxAir, Mathf.InverseLerp(0f, Settings.AirTankMaxFill, _air));
        }
    }

    private int _holes;
    private int Holes
    {
        get => _holes;
        set
        {
            _holes = Mathf.Clamp(value, 0, Settings.MaxAirTankHoles);
            _ui.SetHoles(_holes);

            if (_holes == 0)
            {
                AudioManager.Stop("Leaking Air");
            }
            else
            {
                AudioManager.Play("Leaking Air", Mathf.InverseLerp(0, Settings.MaxAirTankHoles, Holes));
            }
        }
    }

    private AirTankUI _ui;
    private Rigidbody2D rb2d;
    private PlayerMovement player;
    private bool venting;

    private List<AirFillArea> insideAirFills = new List<AirFillArea>();

    private void Start()
    {
        _ui = InGameUI.instance.GetComponentInChildren<AirTankUI>();
        rb2d = GetComponent<Rigidbody2D>();
        Air = Settings.StartingAirTankFill;
        player = GetComponent<PlayerMovement>();
    }

    public void Vent(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            venting = true;
        }
        else if (ctx.canceled)
        {
            venting = false;
        }
    }

    private void Update()
    {
        var deplete_t = Mathf.InverseLerp(0f, Settings.MaxAirTankHoles, Holes);
        var depleteRate = Mathf.Lerp(Settings.AirTankMinAutoDepleteRate, Settings.AirTankMaxAutoDepleteRate, deplete_t);

        Air -=  depleteRate * Time.deltaTime;
        if (venting)
            Air -= Settings.VentAirTankRate * Time.deltaTime;

        if (insideAirFills.Count > 0 && Keyboard.current.eKey.isPressed)
        {
            Air += insideAirFills[0].GetAir();
        }

        if (Air == 0f)
        {
            InGameUI.ShowGameOverScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<AirFillArea>(out var airFill))
        {
            insideAirFills.Add(airFill);
        }

        if (other.gameObject.TryGetComponent<DamageObject>(out var damage))
        {
            Damaged();
            player.Knockback(damage.transform);
            player.anim.PlayHurt();
        }

        if (other.gameObject.TryGetComponent<RepairPickup>(out var repairPickup))
        {
            if (Holes == 0)
                return;

            Destroy(repairPickup.gameObject);
            Holes--;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<AirFillArea>(out var airFill))
        {
            insideAirFills.Remove(airFill);
        }
    }

    public void Fill(float airAmount)
    {
        Air += airAmount;
        AudioManager.Play("Get Air");
    }

    public void Damaged()
    {
        Holes++;
        AudioManager.Play("Lose Air");
    }
}