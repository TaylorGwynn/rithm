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
    private Vector3 popVector;
    private Vector3 popRotVector;
    public float MAXPUSH = 6;
    public float MAXROT = 4;
    // Start is called before the first frame update
    void Start()
    {
        shouldBeAt = note.tick/4f/timer.BPM*60f; // 16ths /4 = beats, /BPM = minutes, *60 = seconds
        // this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        popVector = new Vector3(Random.Range(-MAXPUSH/2,MAXPUSH/2), MAXPUSH*3, 5f);
        popRotVector = new Vector3(Random.Range(-MAXROT,MAXROT), Random.Range(-MAXROT,MAXROT),Random.Range(-MAXROT,MAXROT));
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public float getCurrentDifference(){
        return shouldBeAt - timer.timer;
    }

    public void explode(string accuracy=null){
        Rigidbody rb = this.GetComponent<Rigidbody>();
        // this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        // print("BOOM!");
        // rb.isKinematic = true;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(popVector, ForceMode.Impulse);
        rb.AddTorque(popRotVector, ForceMode.Impulse);
        Destroy(this.gameObject,4);

    }

    //removes the object, with a sad fadeout if it was a miss
    public void die(bool miss){
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        // this.GetComponent<Renderer>().enabled = false;
        Destroy(this.gameObject,4);
    }

}
