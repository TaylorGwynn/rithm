using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatVisualize : timerVisualize
{
    protected Vector3 origin;
    // // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        origin = this.transform.position;
        // print('m');
    }

    // Update is called once per frame
    void Update()
    {

        if (timer.sixteenth == nextNoteVal && nextNoteIdx < song.notes.Count ){
            newVector = origin + util.randomVector();
            nextNoteVal = song.notes[nextNoteIdx++].sixteenth;
        }
        this.transform.position = newVector;

    }
}
