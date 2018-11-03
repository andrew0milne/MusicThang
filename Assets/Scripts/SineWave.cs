using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour {

    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency1;

    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency2;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;

    AudioSource audioSource;
    public int timeIndex = 0;

    public int maxTimeIndex = 0;

    public float bpm = 140.0f;
    private float bpmInSeconds;

    private double nextTick = 0.0f;

    public float pitch;

    public string[] beats;
    int count = 0;

    public GameObject cube;

    public float num;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop(); //avoids audiosource from starting to play automatically

        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick;// * sampleRate;
        bpmInSeconds = (60.0f / bpm) / 2.0f;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        audioSource.pitch = pitch;
    }

    float GetFreq(string name)
    {
        switch(name)
        {
            case "A":
                return 110.0f;
            case "A#":
                return 116.54f;
            case "B":
                return 123.47f;
            case "C":
                return 130.81f;
            case "C#":
                return 138.59f;
            case "D":
                return 146.83f;
            case "D#":
                return 155.56f;
            case "E":
                return 164.81f;
            case "F":
                return 174.61f;
            case "F#":
                return 185.0f;
            case "G":
                return 196.0f;
            case "G#":
                return 207.65f;
            case "AA":
                return 220.0f;

        }

        return 0.0f;
    }

    IEnumerator LowerVolume()
    {
        float time = 0.0f;

        AudioSettings.dspTime


        while (time < bpmInSeconds)
        {
            audioSource.volume = Mathf.Lerp(0.2f, 0.0f, time);

            yield return null;
        }

        yield return null;
    }

    void Update()
    {
        if (AudioSettings.dspTime >= nextTick)
        {
            if (beats[count] != "")
            {
                frequency1 = GetFreq(beats[count]);
                frequency2 = frequency1;
                timeIndex = 0;  //resets timer before playing sound
                audioSource.Play();
                cube.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                audioSource.Stop();
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

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(timeIndex, frequency1, sampleRate);

            if (channels == 2)
                data[i + 1] = CreateSine(timeIndex, frequency2, sampleRate);

            timeIndex++;

            if (timeIndex > maxTimeIndex)
                maxTimeIndex = timeIndex;

            //if timeIndex gets too big, reset it to 0
            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }

    //Creates a sinewave
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        

        num = Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);

        if (maxTimeIndex != 0.0f && timeIndex != 0.0f)
        {
            float scaleValue = timeIndex / maxTimeIndex;

            //num = num * (1.0f - scaleValue);
        }
        //float num2 = Mathf.PerlinNoise(2.0f * Mathf.PI * timeIndex * frequency / sampleRate, 0.0f);

        return num;

        //return (num + num2) / 2.0f;
        //return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }
}
