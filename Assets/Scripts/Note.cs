/*
Class for tracking musical notes in the song: location, beat etc
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Note
{

    public enum NoteType{
        normal,
        holdStart,
        holdEnd
    }
    
    public int getBarNumber(){
        return this.tick/16;
    }
    public int getBarHalf(){
        return this.tick/8 %2;
    }
    public int getBarQuarter(){
        return this.tick/4 %4;
    }
    public int getBarEighth(){
        return this.tick/2 %8;
    }
    public int getBarSixteenth(){
        return this.tick %16;
    }
    public bool isWhole(){
        return tick%16 == 0;
    }
    public bool isHalf(){
        return tick%8 == 0;
    }
    public bool isQuarter(){
        return tick%4 == 0;
    }
    public bool isEighth(){
        return tick%2 == 0;
    }
    public bool isSixteenth(){
        return true;
    }

    public int tick;
    public NoteType type;

    public Note(int sixteenth)
    {
        this.tick = sixteenth;
        this.type = NoteType.normal;
    }

    public Note(int sixteenth, string ntype){
        this.tick = sixteenth;
        try{
            this.type = (NoteType)NoteType.Parse(typeof(NoteType),ntype,true);
        }
        catch (ArgumentException) {
            Debug.Log(ntype + " is not a valid note type.");
            throw;
        }
    }

    public static int CompareByTick(Note note1, Note note2){
        return note1.tick.CompareTo(note2.tick);
    }

}
