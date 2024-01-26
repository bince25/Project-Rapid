using System;
using System.Collections.Generic;
using UnityEngine;


public class AudioSourceWrapper
{
    public AudioSource AudioSource { get; set; }
    public MusicTrack? CurrentTrack { get; set; } = null; // Nullable to indicate it might not be playing anything
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<AudioSourceWrapper> musicSources = new List<AudioSourceWrapper>();
    [SerializeField] private List<AudioSource> sfxSourcesPool = new List<AudioSource>();
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private int sfxPoolSize = 10;
    [SerializeField] private int musicSourcesCount = 2; // Default count for music AudioSource components

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSFXPool();
            InitializeMusicSources(musicSourcesCount);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes the pool of AudioSource components for SFX.
    /// </summary>
    private void InitializeSFXPool()
    {
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSourcesPool.Add(sfxSource);
        }
    }

    /// <summary>
    /// Initializes the list of AudioSourceWrapper objects for music.
    /// </summary>
    /// <param name="count"></param>
    private void InitializeMusicSources(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AudioSourceWrapper wrapper = new AudioSourceWrapper
            {
                AudioSource = gameObject.AddComponent<AudioSource>(),
                CurrentTrack = null
            };
            wrapper.AudioSource.playOnAwake = false;
            musicSources.Add(wrapper);
        }
    }

    /// <summary>
    /// Plays the specified music track on the specified music source.
    /// </summary>
    /// <param name="track"></param>
    /// <param name="loop"></param>
    /// <param name="sourceIndex"></param>
    public void PlayMusic(MusicTrack track, bool loop = true, int sourceIndex = 0)
    {
        if (sourceIndex < 0 || sourceIndex >= musicSources.Count)
        {
            Debug.LogWarning("Invalid music source index.");
            return;
        }

        AudioClip clip = GetMusicClip(track);
        if (clip != null)
        {
            AudioSourceWrapper wrapper = musicSources[sourceIndex];
            wrapper.AudioSource.clip = clip;
            wrapper.AudioSource.loop = loop;
            wrapper.AudioSource.Play();
            wrapper.CurrentTrack = track;
        }
        else
        {
            Debug.LogWarning("Music track not found: " + track.ToString());
        }
    }

    /// <summary>
    /// Stops the specified music track on the specified music source.
    /// </summary>
    /// <param name="track"></param>
    public void StopMusic(MusicTrack track)
    {
        foreach (var wrapper in musicSources)
        {
            if (wrapper.CurrentTrack == track && wrapper.AudioSource.isPlaying)
            {
                wrapper.AudioSource.Stop();
                wrapper.CurrentTrack = null; // Reset the track info
            }
        }
    }

    /// <summary>
    /// Stops the specified music track on the specified music source.
    /// </summary>
    /// <param name="track"></param>
    /// <param name="fadeDuration"></param>
    public void StopMusicWithFade(MusicTrack track, float fadeDuration)
    {
        foreach (var wrapper in musicSources)
        {
            if (wrapper.CurrentTrack == track && wrapper.AudioSource.isPlaying)
            {
                StartCoroutine(FadeOut(wrapper.AudioSource, fadeDuration));
                wrapper.CurrentTrack = null; // Reset the track info
            }


        }
    }

    /// <summary>
    /// Stops the specified music track on the specified music source with default value defined in constants.
    /// </summary>
    /// <param name="track"></param>
    public void StopMusicWithFade(MusicTrack track)
    {
        foreach (var wrapper in musicSources)
        {
            if (wrapper.CurrentTrack == track && wrapper.AudioSource.isPlaying)
            {
                StartCoroutine(FadeOut(wrapper.AudioSource, GameConstants.DEFAULT_FADE_DURATION));
                wrapper.CurrentTrack = null; // Reset the track info
            }


        }
    }

    /// <summary>
    /// Creates a coroutine to fade out the specified audio source over the specified duration.
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="fadeDuration"></param>
    /// <returns></returns>
    private IEnumerator<WaitForSeconds> FadeOut(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    /// <summary>
    /// Stops the specified music track on the specified music source if it is playing.
    /// </summary>
    /// <returns></returns>
    private AudioSource GetPooledSFXSource()
    {
        foreach (AudioSource source in sfxSourcesPool)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null; // Consider expanding the pool if null is frequently returned
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="track"></param>
    /// <returns></returns>
    public AudioClip GetMusicClip(MusicTrack track)
    {
        int index = (int)track;
        return index >= 0 && index < musicClips.Length ? musicClips[index] : null;
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="sfx"></param>
    /// <returns></returns>
    public AudioClip GetSFXClip(SFX sfx)
    {
        int index = (int)sfx;
        return index >= 0 && index < sfxClips.Length ? sfxClips[index] : null;
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        foreach (var wrapper in musicSources)
        {
            wrapper.AudioSource.volume = volume;
        }
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="track"></param>
    public void SetMusicVolume(float volume, MusicTrack track)
    {
        foreach (var wrapper in musicSources)
        {
            if (wrapper.CurrentTrack == track)
            {
                wrapper.AudioSource.volume = volume;
            }
        }
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="volume"></param>
    public void SetSFXVolume(float volume)
    {
        foreach (AudioSource sfxSource in sfxSourcesPool)
        {
            sfxSource.volume = volume;
        }
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="sfx"></param>
    public void SetSFXVolume(float volume, SFX sfx)
    {
        foreach (AudioSource sfxSource in sfxSourcesPool)
        {
            if (sfxSource.clip == GetSFXClip(sfx))
            {
                sfxSource.volume = volume;
            }
        }
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="track"></param>
    public void PauseMusic(MusicTrack track)
    {
        foreach (var wrapper in musicSources)
        {
            if (wrapper.CurrentTrack == track && wrapper.AudioSource.isPlaying)
            {
                wrapper.AudioSource.Pause();
            }
        }
    }

    /// <summary>
    /// Plays the specified SFX clip on a pooled AudioSource component.
    /// </summary>
    /// <param name="track"></param>
    public void ResumeMusic(MusicTrack track)
    {
        foreach (var wrapper in musicSources)
        {
            if (wrapper.CurrentTrack == track && !wrapper.AudioSource.isPlaying)
            {
                wrapper.AudioSource.Play();
            }
        }
    }
}