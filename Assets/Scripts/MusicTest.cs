using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour 
{

public float BPM;
public bool[] hits;
public AudioSource audio;

float beat_interval;
float time;

	// Use this for initialization
	void Start () 
	{
		beat_interval = 60.0f / BPM;
	}
	
	// Update is called once per frame
	void Update () 
	{
		time += Time.deltaTime;

if(time >= beat_interval)
{

time = 0.0f;
audio.Play();
}
	}
}
