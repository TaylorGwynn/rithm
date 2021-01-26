/*
Holds musical notes, parses inputs to create notes
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BeatSize{
    whole = 16,
    half = 8,
    quarter = 4,
    eighth = 2,
    sixteenth = 1
}

[Serializable]
public class Song {
    public string songName;
    public List<Note> notes = new List<Note>() ;
    const int FRONTPADDING = 16;
    // creates a song with a list of notes with any characters/spaces, indicating either
    //  "whole", "half", "quarter", "eighth" or "sixteenth" notes/rests respectively.
    public Song(string fromType, string rawSong){
        
        BeatSize bSize = BeatSize.sixteenth;
        try{
        bSize = (BeatSize)Enum.Parse(typeof(BeatSize),fromType,true);
        }catch(ArgumentException){
        Debug.Log("Unknown type in Song(fromType) '"+fromType+"', using 16ths.");
        }

        int index = 0+FRONTPADDING;
        string type = "normal";
        float xPos = 0;
        foreach (char c in rawSong)
        {
            // TODO change "any character" to match different types of notes
            if (c > 48 && c < 57){
                xPos = (c-48 - 5)/5;
                this.notes.Add(new Note(index,type,xPos));
            }

            index += (int)bSize;
        }
        this.sortByTick();
    }
    // creates a song with a list of notes with any characters/spaces, indicating their length
    // as a multiple of 16th notes/rests respectively.
    public Song(int beatLenSixteenths, string rawSong){
        
        int index = 0+FRONTPADDING;
        string type = "normal";
        float xPos = 0;
        foreach (char c in rawSong)
        {
            // TODO change "any character" to match different types of notes
            if (c > 48 && c < 57){
                xPos = (c-48 - 5)/5f;
                this.notes.Add(new Note(index,type,xPos));
            }

            index += beatLenSixteenths;
        }
        this.sortByTick();
    }

    // sorts this list by ascending order of its notes' tick value
    public void sortByTick(){
        this.notes.Sort(Note.CompareByTick);
    }

}

