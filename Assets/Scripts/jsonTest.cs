using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note{
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

public class Song{
    public enum BeatSize{
        whole = 16,
        half = 8,
        quarter = 4,
        eighth = 2,
        sixteenth = 1
    }
    public string name {get; set;}
    public List<Note> notes {get; set;}

    // creates a song with a list of notes with any characters/spaces, indicating either
    //  "whole", "half", "quarter", "eighth" or "sixteenth" notes/rests respectively.
    public Song(string fromType, string fromBeats){
        
        BeatSize bSize = BeatSize.sixteenth;
        try{
        bSize = (BeatSize)Enum.Parse(typeof(BeatSize),fromType,true);
        }catch(ArgumentException){
        Debug.Log("Unknown type in Song(fromType) '"+fromType+"', using 16ths.");
        }

        long index = 0;
        foreach (char c in fromBeats)
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
    public Song(long beatLenSixteenths, string fromBeats){
        
        long index = 0;
        foreach (char c in fromBeats)
        {
            // TODO change "any character" to match different types of notes
            if(c != ' '){
                this.notes.Add(new Note(index));
            }
            index += beatLenSixteenths;
        }
    }
}

public class jsonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Song testsong = new Song("eighth","1 1 111 1111 11 ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
