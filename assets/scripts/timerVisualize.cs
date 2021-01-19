using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerVisualize : MonoBehaviour
{
    public int note_type = 1;
    public songTimer timer;
    public Vector3 myVector;
    // Start is called before the first frame update
    void Start()
    {
        myVector.x = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {   
        switch (note_type)
        {
            case 1:
                myVector.y = (float)timer.bar           /1 ;
                break;
            case 2:
                myVector.y = (float)timer.half       %2   /2 ;
                break;
            case 4:
                myVector.y = (float)timer.quarter    %4   /4 ;
                break;
            case 8:
                myVector.y = (float)timer.eighth     %8   /8 ;
                break;
            case 16:
                myVector.y = (float)timer.sixteenth   %16  /16 ;
                break;
            
            default:
                break;
        }

    this.transform.position = myVector;
      
        
    }
}
