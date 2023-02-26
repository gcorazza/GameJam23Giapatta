using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundmusic : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource[] backgroundmusics;
    private int playing = 0;
    void Start()
    {
        backgroundmusics = GetComponents<AudioSource>();
        backgroundmusics[playing].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!backgroundmusics[playing].isPlaying)
        {
            playing++;
            if (playing ==2)
            {
                playing = 0;
            }
            backgroundmusics[playing].Play();
        }
    }
}
