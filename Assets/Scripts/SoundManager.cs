using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip Explosion;

    AudioSource Explode;
    
    public static SoundManager instance;

    void Awake()
    {
        if (SoundManager.instance == null)
            SoundManager.instance = this;
    }

    void Start()
    {
        Explode = GetComponent<AudioSource>();
    }

    public void PlayExplodeSound()
    {
        if (Explode.GetComponent<AudioSource>().isPlaying)
            return;
        else
            Explode.GetComponent<AudioSource>().PlayOneShot(Explosion);
    }

}
