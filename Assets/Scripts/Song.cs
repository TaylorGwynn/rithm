﻿using System.Collections;
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
public class Song {
    public string songName {get; set;}
    public List<Note> notes = new List<Note>() ;

    // creates a song with a list of notes with any characters/spaces, indicating either
    //  "whole", "half", "quarter", "eighth" or "sixteenth" notes/rests respectively.
    public Song(string fromType, string rawSong){
        
        BeatSize bSize = BeatSize.sixteenth;
        try{
        bSize = (BeatSize)Enum.Parse(typeof(BeatSize),fromType,true);
        }catch(ArgumentException){
        Debug.Log("Unknown type in Song(fromType) '"+fromType+"', using 16ths.");
        }

        long index = 0;
        foreach (char c in rawSong)
        {
            // TODO change "any character" to match different types of notes
            if(c != ' '){
                this.notes.Add(new Note(index));
            }
            index += (long)bSize;
        }
    }
    // creates a song with a list of notes with any characters/spaces, indicating their length
    // as a multiple of 16th notes/rests respectively.
    public Song(int beatLenSixteenths, string rawSong){
        
        long index = 0;
        foreach (char c in rawSong)
        {
            // TODO change "any character" to match different types of notes
            if(c != ' '){
                this.notes.Add(new Note(index));
            }
            index += beatLenSixteenths;
        }
    }

}

