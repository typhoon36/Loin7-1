using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audioSource;
    public AudioClip soundBullet; //재생할 소리 변수
    public AudioClip soundDie; //죽는 사운드
    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundDie()
    {
        audioSource.PlayOneShot(soundDie);
    }

    public void SounBullet()
    {
        audioSource.PlayOneShot(soundBullet);
    }

}
