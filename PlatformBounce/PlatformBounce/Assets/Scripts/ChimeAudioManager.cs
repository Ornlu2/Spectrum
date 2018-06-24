using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeAudioManager : MonoBehaviour {


    public AudioClip[] PlatformChimes;
    public AudioClip BlackPlatformChime;
    public AudioClip NeutralPlatformChime;
    private AudioSource Source;

    private void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
    }

    public void PlatformColSoundEffect()
    {
        Source.clip = PlatformChimes[Random.Range(0, PlatformChimes.Length)];
        Source.Play();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Neutral_Platform")
        {
            Source.clip = NeutralPlatformChime;
            Source.Play();
        }
        else if(collision.gameObject.tag == "Black_Platform")
        {
            Source.clip = BlackPlatformChime;
            Source.Play();
        }
    }
}
