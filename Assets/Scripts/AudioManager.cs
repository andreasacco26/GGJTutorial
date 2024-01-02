using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [Header("====Audio Sources====")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("====Audio Clips====")]
    [SerializeField] List<Sound> sfxSounds;
    [SerializeField] Sound musicSound;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        PlayMusic();
        ToggleMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = musicSound.clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySfx(string sfxName)
    {
        Sound sound = sfxSounds.Find((x) => x.name == sfxName);
        if (sfxName == null)
        {
            Debug.Log("Cannot reproduce sound: " + sfxName + " not found!");
            return;
        }
        sfxSource.PlayOneShot(sound.clip);
    }

    private void ToggleSource(AudioSource source) => source.mute = !source.mute;
    public void ToggleMusic() => ToggleSource(musicSource);
    public void ToggleSfx() => ToggleSource(sfxSource);

    private void Volume(AudioSource source, float volume) => source.volume = volume;
    public void MusicVolume(float volume) => Volume(musicSource, volume);
    public void SfxVolume(float volume) => Volume(sfxSource, volume);
}
