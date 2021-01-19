using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerVisualize : MonoBehaviour
{
    public int note_type = 1;
    public songTimer timer;
    Song song;
    Vector3 newVector;
    long nextNoteVal;
    int nextNoteIdx = 0;
    // Start is called before the first frame update
    void Start()
    {
        newVector.x = this.transform.position.x;
        song = new Song("eighth","1 1 111 1111 11 ");
        nextNoteVal = song.notes[nextNoteIdx].sixteenth;
    }

    // Update is called once per frame
    void Update()
    {   
        switch (note_type)
        {
            case 1:
                newVector.y = (float)timer.getBar()           /1 ;
                break;
            case 2:
                newVector.y = (float)timer.getHalf()       %2   /2 ;
                break;
            case 4:
                newVector.y = (float)timer.getQuarter()    %4   /4 ;
                break;
            case 8:
                newVector.y = (float)timer.getEighth()    %8   /8 ;
                break;
            case 16:
                newVector.y = (float)timer.sixteenth   %16  /16 ;
                break;

            //special test case: vibrates in time with given song
            case 0:
                if (timer.sixteenth == nextNoteVal){
                    newVector.x = (newVector.x +1)%2;
                    nextNoteIdx++;
                    nextNoteVal = song.notes[nextNoteIdx].sixteenth;
                }break;

            default:
                break;
        }

    this.transform.position = newVector;
      
        
    }
}
