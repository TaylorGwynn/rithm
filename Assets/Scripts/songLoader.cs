/*
Handles loading of the song file / raw song input, creates song
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class songLoader : MonoBehaviour
{
    public TextAsset songFile;
    public string rawSong = "1 1 111 ";
    public BeatSize size = BeatSize.quarter;
    public string songPath;
    public Song song;
    // Start is called before the first frame update
    void Start()
    {
        if (songFile == null){        
            this.song = new Song((int)size, rawSong);
        }else{
            this.song = new Song((int)size, songFile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
