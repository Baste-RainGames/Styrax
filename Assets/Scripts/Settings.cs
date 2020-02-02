using System.Linq;
using UnityEngine;

public class Settings : ScriptableObject
{
    [Header("Camera")]
    public float cameraFollowSpeed;
    public static float CameraFollowSpeed => instance.cameraFollowSpeed;

    [Header("Player")]
    public float playerXMovement;
    public static float PlayerXMovement => instance.playerXMovement;
    public float playerYMovement;
    public static float PlayerYMovement => instance.playerYMovement;
    public float playerMaxXVel;
    public static float PlayerMaxXVel => instance.playerMaxXVel;
    public float playerMaxYVel;
    public static float PlayerMaxYVel => instance.playerMaxYVel;

    public float playerGravityNoAir;
    public static float PlayerGravityNoAir => instance.playerGravityNoAir;
    public float playerGravityMaxAir;
    public static float PlayerGravityMaxAir => instance.playerGravityMaxAir;

    [Header("Air Tank")]
    public float startingAirTankFill;
    public static float StartingAirTankFill => instance.startingAirTankFill;
    public float airTankMaxAutoDepleteRate;
    public static float AirTankMaxAutoDepleteRate => instance.airTankMaxAutoDepleteRate;
    public float airTankMinAutoDepleteRate;
    public static float AirTankMinAutoDepleteRate => instance.airTankMinAutoDepleteRate;
    public float airTankMaxFill;
    public static float AirTankMaxFill => instance.airTankMaxFill;
    public float ventAirTankRate;
    public static float VentAirTankRate => instance.ventAirTankRate;

    [Header("Damage")]
    public float damageKnockbackForce = 10f;
    public static float DamageKnockbackForce => instance.damageKnockbackForce;
    public int maxAirTankHoles = 5;
    public static int MaxAirTankHoles => instance.maxAirTankHoles;
    public float invincibleForSeconds = .5f;
    public static float InvincibleForSeconds => instance.invincibleForSeconds;

    [Header("Other")]
    public float parallax = .5f;
    public static float Parallax => instance.parallax;

    public float crabAttackPlayerDistance = 5f;
    public static float CrabAttackPlayerDistance => instance.crabAttackPlayerDistance;

    void OnEnable()
    {
        _instance = this;
    }

    private static Settings _instance;
    private static Settings instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Settings instance null, fixing");
                _instance = Resources.Load<Settings>("Settings");
            }

            return _instance;
        }
    }
}