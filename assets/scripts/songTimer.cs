using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songTimer : MonoBehaviour
{
    public int bar, half, quarter, eighth, sixteenth;
    private float timer;
    public int BPM = 120;
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
        timer += Time.deltaTime;
        bar         = Mathf.FloorToInt( timer /4 / bpm_scale );
        half        = Mathf.FloorToInt( timer /2 / bpm_scale );
        quarter     = Mathf.FloorToInt( timer    / bpm_scale );
        eighth      = Mathf.FloorToInt( timer *2 / bpm_scale );
        sixteenth   = Mathf.FloorToInt( timer *4 / bpm_scale );
        // print(bar);
    }
    void restart(){
        timer = bar = half = quarter = eighth = sixteenth = 0;
    }
}

// 120 BPM (beats per minute)

// 1/120 minutes per beat
// 60/120 = 1/2
// 0.5 seconds per beat

// 200 BPM
// 1/200 MPB
// 60/200 = 1/3 = 0.33 seconds per beat