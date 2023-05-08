using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;

    static AudioManager instance;
    //[SerializeField] AudioSource getCoinSFX;
    //[SerializeField] AudioSource getEagleSFX;

    public bool IsMute { get => bgm.mute; }
    public float BgmVolume { get => bgm.volume; }

    private void Awake()
    {
        if(instance != null)
        {
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgm.isPlaying)
            bgm.Stop();

        bgm.clip = clip;
        bgm.loop = loop;
        bgm.Play();
    }

    //public void playGetCoinSFX(AudioClip clip)
    //{
    //    if (getCoinSFX.isPlaying)
    //        getCoinSFX.Stop();

    //    getCoinSFX.clip = clip;
    //    getCoinSFX.Play();
    //}

    //public void playGetEagleSFX(AudioClip clip)
    //{
    //    //if (!alreadyPlayed)
    //    //{
    //    //    //eagleSound.PlayOneShot(eagleClip);
    //    //    alreadyPlayed = true;
    //    //}

    //    if (getEagleSFX.isPlaying)
    //        getEagleSFX.Stop();

    //    getEagleSFX.clip = clip;
    //    getEagleSFX.Play();
    //}

    public void SetMute(bool value)
    {
        bgm.mute = value;
    }

    public void SetBgmVolume(float value)
    {
        bgm.volume = value;
    }


}
