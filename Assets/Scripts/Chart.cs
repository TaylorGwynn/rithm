/*
Generates notes and scrolls up, 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : MonoBehaviour
{
    public songTimer timer;
    public songLoader songLoader;
    protected Song song ;
    protected GameObject songTop;
    private Material grid;

    Quaternion rot = new Quaternion(0,0,0,0);
    Vector3 chartTop;
    public Material quarterMaterial;
    public Material eighthMaterial;
    public Material sixteenthMaterial;
    public NoteBlock noteBlockToSpawn;
    public int LOADEDBARS = 2;      // # of bars loaded ahead of time (offscreen)
    public int TOOLATE = 4;         // # of ticks that a note's considered "too late" to intend being hit
    public int OKWINDOW = 3;        // # of ticks that a note's considered "hit"

    private Note nextN;
    private int nextNIndex;

    private List<NoteBlock> noteBlocks;
    private int topNote = 0;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 po = this.transform.position;
        chartTop = new Vector3(po.x, po.y + this.transform.localScale.y /2);
        songTop = Instantiate(new GameObject(), chartTop, rot);
        grid = this.GetComponent<Renderer>().material;
        //TODO make songPlayer superclass?
        if (timer == null){
            timer = (songTimer)GameObject.Find("SongSource").GetComponent(typeof(songTimer));
        }
        if (songLoader == null){
            print("No song detected for "+this.gameObject.name+", using placeholder song.");
            song = new Song("eighth","1 1 111 1111 11 ");
        }else{
            song = songLoader.song;
        }
        quarterMaterial = (quarterMaterial == null? (Material)Resources.Load("Assets/materials/Blue",typeof(Material)) : quarterMaterial);
        eighthMaterial = (eighthMaterial == null? (Material)Resources.Load("Red",typeof(Material)) : eighthMaterial);
        if (eighthMaterial == null){
            eighthMaterial = (Material)Resources.Load("Red",typeof(Material));
        }
        sixteenthMaterial = (sixteenthMaterial == null? (Material)Resources.Load("Green",typeof(Material)) : sixteenthMaterial);

        noteBlocks = new List<NoteBlock>();
        nextNIndex = 0;
        nextN = song.notes[nextNIndex++];

        //optimization: only fill out first four bars
        spawnNoteBlocks(0);
        spawnNoteBlocks(32);

    }

    // Update is called once per frame
    void Update()
    {
        if (timer.playing){
            scrollAll(Time.deltaTime*(timer.BPM/240f)); // BPM/240 =  BPM/ beats*4*60: bars per second
            //every bar is 1 unit of scroll
            //1 beat = 0.25 unit, one 16th i.e. tick = 0.0625 unit
        }
        //optimization: every 2 bars, spawn two more bars off the end, 2 bars in the future
        if ((timer.sixteenth + 16) % 32 == 0){
            spawnNoteBlocks(timer.sixteenth + 2*16);
        }

        //TODO is this checking too often?
        if(timer.sixteenth % 1 == 0 && topNote < noteBlocks.Count ){
            // print("purging...?");
            purgeFinishedNotes();
        }
        
    }

    //spawns LOADEDBARS # of bars of notes starting at the given tick
    // assumes notes are in ascending order of tick value
    void spawnNoteBlocks(int tick){
        Vector3 notePos;
        float dist;
        // float toChartTop = this.transform.localScale.y /2 + grid.GetTextureOffset("_MainTex").y;
        float toChartTop = this.transform.localScale.y /2f;
        
        NoteBlock curr;
        // foreach (Note n in song.notes){
        while (nextN.tick < (tick + 16*(LOADEDBARS + 1)) && nextNIndex < song.notes.Count ){
            dist = -( (nextN.tick-timer.sixteenth)*(toChartTop/16) );
            // print("nextN.tick: "+nextN.tick);
            // TODO can change x pos, and add types here
            notePos = new Vector3(0, dist + toChartTop, -1) + this.transform.position;
            // print("tick: "+n.tick);
            // TODO optimize by only creating this bar + next... put in Update()...
            curr = Instantiate(noteBlockToSpawn, notePos, rot);
            curr.transform.parent = songTop.transform;
            curr.note = nextN;
            if (nextN.isQuarter()){
                // curr.GetComponent<Renderer>().material = quarterMaterial;
                curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.blue);
            }else if(nextN.isEighth()){
                // curr.GetComponent<MeshRenderer>().material = eighthMaterial;
                curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.red);
            }else if(nextN.isSixteenth()){
                // curr.GetComponent<Renderer>().material = sixteenthMaterial;
                curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.green);
            }
            noteBlocks.Add(curr);
            nextN = song.notes[nextNIndex++];
        }
        // print("END CHUNK: currently tick "+timer.sixteenth);
        return;
    }

    void scrollAll(float y){
        scrollTex(y);
        scrollNotes(y);
    }
    void scrollTex(float y){
        Vector2 currOffset = grid.GetTextureOffset("_MainTex");
        grid.SetTextureOffset("_MainTex",currOffset + new Vector2(0,-y));
    }
    void scrollNotes(float y){
        songTop.transform.Translate(0,y*this.transform.localScale.y/2,0);
    }

    public void correctScroll(int ticks){
        scrollAll(ticks*0.0625f); // 0.0625 = 1/16
        timer.correct(ticks);
    }
    public void correctScroll(float seconds){
        scrollAll(seconds * (timer.BPM/240));// BPM/240: bars per second
        timer.correct(seconds);
    }

    //TODO!!!! don't scan through notes sequentially, here or above...
    private void purgeFinishedNotes(){
        // print("topnote: "+topNote+" currdiff "+noteBlocks[topNote].getCurrentDifference());
        // clear out the missed notes
        print("-TOOLATE/4f/timer.BPM*60f: "+(-TOOLATE/4f/timer.BPM*60f));
        while (noteBlocks[topNote].getCurrentDifference() < -TOOLATE/4f/timer.BPM*60f){
            print("purged "+topNote);
            noteBlocks[topNote++].die(miss:true);
        }
        // clear out the hit notes
        while (noteBlocks[0])
    }
    public int hit(string key){
        if (noteBlocks[topNote].getCurrentDifference() < OKWINDOW/4f/timer.BPM*60f){
            noteBlocks[topNote++].explode();
            return 0;
        }return -1;

    }
}
