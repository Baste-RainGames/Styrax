using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    /* Dette scriptet trenger bare å ligge på et
    eget game object, for ordens skyld. */

    /* Bruk 'AudioManager.Play("lydnavn");' på
    eventene du vil at lyden skal spilles av. */

    /* Bruk 'AudioManager.Stop("lydnavn");' på
    eventene du vil at lyden skal stoppes. */

    [ArrayElementTitle(nameof(Sound.clip))]
    public Sound[] sounds;

    [RuntimeInitializeOnLoadMethod]
    public static void EnsureAudioManager()
    {
        instance = Instantiate(Resources.Load<AudioManager>("Audio Manager"));
        instance.name = "[Audio Manager]";
        DontDestroyOnLoad(instance);
    }

    private static AudioManager instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;

        }
    }

    public static void Play(string name, float volumeMultiplier = 1f) => instance.PlayInstance(name, volumeMultiplier);

    public void PlayInstance (string name, float volumeMultiplier = 1f)
    {
        Sound s = Array.Find(sounds, sound => sound.clip != null && sound.clip.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if(s.randomPitch)
        {
            s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        }

        if(s.greaterRandomPitch)
        {
            s.source.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        }

        s.source.volume = s.volume * volumeMultiplier;
        s.source.Play();
    }

    public static void Stop(string name) => instance.StopPlayInstance(name);

    public void StopPlayInstance(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip != null && sound.clip.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

}