/*
Tracks/controls current beat (in ticks = 16th notes) and time elapsed (in seconds). 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songTimer : MonoBehaviour
{
    public int sixteenth;
    public float timer;
    public int BPM = 120;
    public bool playing = true;
    private float bpm_scale;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        bpm_scale = 60f/BPM;
    }

    // Update is called once per frame
    void Update()
    {   
        if(playing){
            // print(Time.deltaTime);
            timer += Time.deltaTime;
            
            sixteenth   = Mathf.FloorToInt( timer *4 / bpm_scale );
            // print(bar);
            
        }
    }
    public int getEighth(){
        return sixteenth/2;
    }
    public int getQuarter(){
        return sixteenth/4;
    }
    public int getHalf(){
        return sixteenth/8;
    }
    public int getBar(){
        return sixteenth/16;
    }

    public void restart(){
        sixteenth = 0;
        playing = true;
        return;
    }

    public void correct(int ticks){
        if (ticks < 0){
            //TODO unset "playing", wait, set back
        }else{
            sixteenth += ticks;
        }
    }
    public void correct(float seconds){
        if (seconds < 0){
            //TODO unset "playing", wait, set back
            throw new System.NotImplementedException("need to add negative (pausing) correction to timer");
        }
        timer += seconds;
        // it will update sixteenth next frame in Update()
    }
    public void pause(){
        playing = false;
        return;
    }
    IEnumerator pauseCoroutine3(){
        playing = false;
        yield return new WaitForSeconds(3);
        playing = true;
    }

}

// 120 BPM (beats per minute)

// 1/120 minutes per beat
// 60/120 = 1/2
// 0.5 seconds per beat

// 200 BPM
// 1/200 MPB
// 60/200 = 1/3 = 0.33 seconds per beat