using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private AirTank airTank;
    public float xInput;

    [NonSerialized] public PlayerAnimation anim;

    //Indicates wether you can jump or not
    private bool canJump = true;

    //Wait time until you can jump again
    public float jumpDelay = 0.5f;
    public bool IsTouchingBottom { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        airTank = GetComponent<AirTank>();
        anim = GetComponent<PlayerAnimation>();

        SetXInput(0);
    }

    public void ReadMovementInput(InputAction.CallbackContext ctx)
    {
        if (InGameUI.isPaused || InGameUI.gameOver)
            return;

        if (ctx.performed)
        {
            var x = ctx.ReadValue<Vector2>().x;
            SetXInput(x);
        }
        else if (ctx.canceled)
        {
            SetXInput(0);
        }
    }

    public void ReadJumpInput(InputAction.CallbackContext ctx)
    {
        if (InGameUI.isPaused || InGameUI.gameOver)
            return;

        if (ctx.performed && canJump)
        {
            AudioManager.Play("Bubble");
            anim.OnJump();
            rb.AddForce(new Vector2(0f, Settings.PlayerYMovement));
            canJump = false;
            StartCoroutine(delayJump());
        }
    }

    private void SetXInput(float x)
    {
        xInput = x;
        if (Mathf.Approximately(xInput, 0f))
            anim.SetInputDir(InputDir.None);
        else if (xInput < 0)
            anim.SetInputDir(InputDir.Left);
        else if (xInput > 0)
            anim.SetInputDir(InputDir.Right);
    }

    public void Knockback(Transform knockbackFrom)
    {
        rb.velocity = default;
        Vector2 dir = (transform.position - knockbackFrom.transform.position).normalized;
        rb.AddForce(dir * Settings.DamageKnockbackForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(xInput * Settings.PlayerXMovement, 0));

        var vel = rb.velocity;
        if (Mathf.Abs(vel.x) > Settings.PlayerMaxXVel)
            vel.x = Settings.PlayerMaxXVel * Mathf.Sign(vel.x);
        if (Mathf.Abs(vel.y) > Settings.PlayerMaxYVel)
            vel.y = Settings.PlayerMaxYVel * Mathf.Sign(vel.y);
        rb.velocity = vel;
    }

    //Resets canJump after the time specified in jumpDelay has elapsed
    IEnumerator delayJump()
    {
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bottom"))
        {
            IsTouchingBottom = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PickupContainer>(out var pickup))
        {
            if (Inventory.Add(pickup))
                Destroy(pickup.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bottom"))
        {
            IsTouchingBottom = false;
        }
    }
}