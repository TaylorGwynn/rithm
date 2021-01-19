using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note
{

    public enum NoteType{
        normal,
        holdStart,
        holdEnd
    }
    
    public long sixteenth;
    public NoteType type;

    public Note(long sixteenth)
    {
        this.sixteenth = sixteenth;
        this.type = NoteType.normal;
    }

    public Note(long sixteenth, string ntype){
        this.sixteenth = sixteenth;
        try{
            this.type = (NoteType)NoteType.Parse(typeof(NoteType),ntype,true);
        }
        catch (ArgumentException) {
            Debug.Log(ntype + " is not a valid note type.");
            throw;
        }
    }

}
