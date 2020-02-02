using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator animator;
    private PlayerMovement player;

    private float hurtDuration = .4f;

    private InputDir inputDir;
    private float hurtTimestamp;
    private float jumpTimestamp;
    private PlayerAnimState playedState;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playedState == PlayerAnimState.Dead)
            return;

        if (playedState == PlayerAnimState.Hurt && Time.time - hurtTimestamp < hurtDuration)
            return;

        PlayerAnimState shouldPlay;
        if (inputDir == InputDir.None)
        {
            if (playedState == PlayerAnimState.Swim_Up && Time.time - jumpTimestamp < .5f)
                shouldPlay = PlayerAnimState.Swim_Up;
            else
                shouldPlay = player.IsTouchingBottom ? PlayerAnimState.Idle_Ground : PlayerAnimState.Idle_Float;
        }
        else
        {
            if (player.IsTouchingBottom)
            {
                shouldPlay = PlayerAnimState.Walk;
            }
            else
            {
                var swimmingAgainstVelocity = (inputDir == InputDir.Right) != (rb2d.velocity.x > 0);
                shouldPlay = swimmingAgainstVelocity ? PlayerAnimState.Swim_Fast : PlayerAnimState.Swim;
            }
        }

        if (shouldPlay != playedState)
            Play(shouldPlay);
    }

    public void SetInputDir(InputDir inputDir)
    {
        this.inputDir = inputDir;

        if (inputDir == InputDir.Left)
            sr.flipX = true;
        else if (inputDir == InputDir.Right)
            sr.flipX = false;
    }

    public void PlayHurt()
    {
        hurtTimestamp = Time.time;
        Play(PlayerAnimState.Hurt);
    }

    public void PlayDead()
    {
        Play(PlayerAnimState.Dead);
    }

    public void OnJump()
    {
        jumpTimestamp = Time.time;
        if (inputDir == InputDir.None)
            Play(PlayerAnimState.Swim_Up);
    }

    private void Play(PlayerAnimState state)
    {
        playedState = state;

        switch (state)
        {
            case PlayerAnimState.Idle_Float:
                animator.Play("Idle Float");
                break;
            case PlayerAnimState.Idle_Ground:
                animator.Play("Idle Stand On Bottom");
                break;
            case PlayerAnimState.Swim:
                animator.Play("Fox Swim");
                break;
            case PlayerAnimState.Swim_Fast:
                animator.Play("Fox Swim Fast");
                break;
            case PlayerAnimState.Hurt:
                animator.Play("Hurt");
                break;
            case PlayerAnimState.Dead:
                animator.Play("Dead");
                break;
            case PlayerAnimState.Swim_Up:
                animator.Play("Fox Swim Up");
                break;
            case PlayerAnimState.Walk:
                animator.Play("Fox Walk");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

}

public enum InputDir
{
    Right,
    Left,
    None
}

public enum PlayerAnimState
{
    Idle_Float,
    Idle_Ground,
    Walk,
    Swim,
    Swim_Fast,
    Hurt,
    Dead,
    Swim_Up
}