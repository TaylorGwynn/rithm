/*
Takes player inputs and checks them against the current song.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public songTimer timer;
    public Chart chart;
    private bool newpress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            chart.hit("");
        }
    }

    // placeholder to test out notes being hit
    public int checkIfHit(){
        return 0;
    }
}
