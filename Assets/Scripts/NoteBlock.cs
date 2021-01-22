/*
The gameplay object of a "Note", contains functions to check if it is hit
 and to act when it is hit
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBlock : MonoBehaviour
{
    public Note note;
    public songTimer timer;
    public Chart chart;
    private float shouldBeAt;
    // Start is called before the first frame update
    void Start()
    {
        shouldBeAt = note.tick/4f/timer.BPM*60f; // 16ths /4 = beats, /BPM = minutes, *60 = seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getCurrentDifference(){
        return shouldBeAt - timer.timer;
    }

    public void explode(){
        this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        print("BOOM!");
    }

    //removes the object, with a sad fadeout if it was a miss
    public void die(bool miss){
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        this.GetComponent<Renderer>().enabled = false;
    }

}
