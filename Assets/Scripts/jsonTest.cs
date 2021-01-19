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
        whole = 1,
        half = 2,
        quarter = 3,
        eighth = 8,
        sixteenth = 16
    }
    public string name {get; set;}
    public List<Note> notes {get; set;}


    // creates a song with a list of notes with any characters/spaces, indicating either
    //  "whole", "half", "quarter", "eighth" or "sixteenth" notes/rests respectively.
    // public Song(string fromType, string fromBeats){
    //     BeatSize size = (BeatSize)Enum.Parse(typeof(BeatSize),fromType,true);
    //     Song(size, fromBeats);
        
    // }

    // creates a song with a list of notes with any characters/spaces, indicating either
    //  "whole", "half", "quarter", "eighth" or "sixteenth" notes/rests respectively.
    public Song(string fromType, string fromBeats){
        
        string t = fromType.ToLower();
        int beatSize; //number to "multiply" to match the number of 16ths each char represents
        if       (t    == "whole" || t == "1"){
            beatSize = 16;
        }else if (t    == "half" || t == "2"){
            beatSize = 8;
        }else if (t    == "quarter" || t == "4"){
            beatSize = 4;
        }else if (t    == "eighth" || t == "8"){
            beatSize = 2;
        }else if (t    == "sixteenth" || t == "16"){
            beatSize = 1;
        } else{
            Debug.Log("Unknown Song(fromType) '"+fromType+"', using 16ths.");
            beatSize = 1;
        }

        int index = 0;
        foreach (char c in fromBeats)
        {
            // TODO change "any character" to match different types of notes
            if(c != ' '){
                this.notes.Add(new Note(index));
            }
            index += beatSize;
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
