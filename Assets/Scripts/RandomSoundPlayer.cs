using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSoundPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource[] audioSources;
    private AudioSource playing;

    public RandomSoundPlayer(AudioSource[] audioSources)
    {
        this.audioSources = audioSources;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        playing = audioSources[Random.Range(0, audioSources.Length)];
        playing.Play();
    }

    public void Stop()
    {
        if (playing == null)
        {
            return;
        }
        playing.Stop();
    }
    public Boolean isPlaying()
    {
        if (playing == null)
        {
            return false;
        }
        return playing.isPlaying;
    }
}
