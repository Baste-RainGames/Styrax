using UnityEngine;

[System.Serializable]

/* Du trenger bare å ha dette scriptet
 * liggende i prosjektet ditt,
 * og trenger ikke å legges til i hierarkiet. 😎 */

public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool loop;

    public bool randomPitch;
    public bool greaterRandomPitch;

    [HideInInspector]
    public AudioSource source;
}

