using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] public AudioSource bgm;
    [SerializeField] public AudioSource fx;
    [Header("-=-AUDIO CLIP - FX-=-")]
    [SerializeField] public AudioClip playerSwing;
    [SerializeField] public AudioClip capsuleHit;
    [SerializeField] public AudioClip coinHit;
    [SerializeField] public AudioClip winMap;
    [SerializeField] public AudioClip explosion;
    [Header("-=-AUDIO CLIP - SOUND-=-")]
    [SerializeField] public AudioClip mainMenu;
    [SerializeField] public AudioClip BGM1;
    [SerializeField] public AudioClip BGM2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bgmPlay(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }
    public void fxPlay(AudioClip clip)
    {
        fx.clip = clip;
        fx.PlayOneShot(clip);
    } 
    public void bgmStop()
    {
        bgm.Stop();
    }
    public void fxStop()
    {
        fx.Stop();
    }
}
