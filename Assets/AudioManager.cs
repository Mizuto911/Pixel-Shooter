using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip gameMusic;
    public AudioClip enemyDeath;
    public AudioClip playerDeath;
    public AudioClip shootMissile;
    public AudioClip bossMusic;

    void Start()
    {
        musicSource = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        sfxSource = gameObject.transform.GetChild(1).GetComponent<AudioSource>();

        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public void playSFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    public void playSFX(AudioClip audioClip, float volume)
    {
        sfxSource.PlayOneShot(audioClip, volume);
    }
}
