/*
Plays and pauses the audio track
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
	private AudioSource audioComp;
	public songTimer timer;
	public int delay_ms = -5;
	// Start is called before the first frame update
	void Start()
	{
		audioComp = GetComponent<AudioSource>();
		if (timer == null){
			timer = (songTimer)GameObject.Find("SongSource").GetComponent(typeof(songTimer));
		}
		// audioComp.enabled = false;
		// waitPadding();

		//TODO investigate how this goes when it's faster etc
		delay_ms += 175; //pre-baked arbitrary number I tuned
		// audioComp.Pause();
		audioComp.PlayDelayed(240/timer.BPM +delay_ms*0.001f); // 240/BPM = seconds per bar, 0.001f: input to milliseconds
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

}
