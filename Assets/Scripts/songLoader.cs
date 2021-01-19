using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class songLoader : MonoBehaviour
{
    public string rawSong = "1 1 111 ";
    public BeatSize size = BeatSize.quarter;
    public string songPath;
    public Song song;
    // Start is called before the first frame update
    void Start()
    {
        this.song = new Song((int)size, rawSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
