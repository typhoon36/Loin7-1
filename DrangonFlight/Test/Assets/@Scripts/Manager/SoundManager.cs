using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Source")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip bgm;
    public AudioClip bulletSfx;
    public AudioClip dieSfx;

     void Awake()
    {
        if (instance == null)
            instance = this;

    }

    void Start()
    {
        PlayBGM();
    }


    public void PlayBGM()
    {
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    //public void StopBGM()
    //{
    //    bgmSource.Stop();
    //}

    public void SoundBullet()
    {
        sfxSource.PlayOneShot(bulletSfx);
    }

    public void SoundDie()
    {
        sfxSource.PlayOneShot(dieSfx);
    }
}
