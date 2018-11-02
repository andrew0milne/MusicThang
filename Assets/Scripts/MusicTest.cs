using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour
{

    public float bpm = 140.0f;
    private float bpmInSeconds;

    private double nextTick = 0.0f;

    private double sampleRate = 0.0f;

    public AudioSource audio;

    public bool[] beats;
    int count = 0;

    public GameObject cube;

    void Start()
    {
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick;// * sampleRate;
        bpmInSeconds = (60.0f / bpm) / 2.0f;

        audio = GetComponent<AudioSource>();

        //beats = new bool[8];
    }

    private void Update()
    {
        if (AudioSettings.dspTime >= nextTick)
        {
            if (beats[count] == true)
            {
                audio.Play();
                cube.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                cube.GetComponent<Renderer>().material.color = Color.black;
            }
            nextTick += bpmInSeconds;
            count++;
            if (count >= beats.Length)
            {
                count = 0;
            }
        }
    }


}
