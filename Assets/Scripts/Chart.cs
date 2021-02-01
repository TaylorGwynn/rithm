/*
Generates notes and scrolls up, controls gameplay (hitting notes, score etc)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour
{
    public songTimer timer;
    public songLoader songLoader;
    public Slider slider;
    protected Song song ;
    protected GameObject songTop;
    private Material grid;
    public GameObject txt;
    public TextMesh cheer;
    // public PlayerInputController PlayerInputController;
    Quaternion noRot = new Quaternion(0,0,0,0);
    Vector3 oneScale = new Vector3(1,1,1);
    private Transform localTransformSansScale;
    Vector3 chartTop;
    // public Material quarterMaterial;
    // public Material eighthMaterial;
    // public Material sixteenthMaterial;
    public Color quarterCol = new Color(1, 0.2f, 0);
    public Color eighthCol = new Color(0, 0.5f, 1);
    public Color sixteenthCol = new Color(0, 0.7f, 0);
    public NoteBlock noteBlockToSpawn;
    public int LOADEDBARS = 2;      // # of bars loaded ahead of time (offscreen)
    public int TOOLATE_TICKS = 2;         // # of ticks that a note's considered "too late" to intend being hit
    private float toolate_seconds;
    public int OKWINDOW_TICKS = 3;        // # of ticks that a note's considered "hit"
    private float okwindow_seconds;

    private Note nextN;
    private int nextNIndex;

    private List<NoteBlock> incomingNoteBlocks;
    private List<NoteBlock> topNotes;
    // private int topNote = 0;

    private float prevNoteX;
    private int prevKeyX;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 po = this.transform.position;
        chartTop = this.transform.rotation*(new Vector3(po.x, po.y + this.transform.localScale.y /2));//NEED TO FIX Y here is just "up", should be "up relative to chart board
        // chartTop = new Vector3(po.x, po.y + this.transform.localScale.y /2);//NEED TO FIX Y here is just "up", should be "up relative to chart board
        songTop = Instantiate(new GameObject(), chartTop, this.transform.rotation, this.transform);
        songTop.transform.localScale = util.divideScales(oneScale,this.transform.localScale);
        grid = this.GetComponent<Renderer>().material;
        grid.SetTextureOffset("_MainTex", new Vector2(0,0));
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
        // quarterMaterial = (quarterMaterial == null? (Material)Resources.Load("Assets/materials/Blue",typeof(Material)) : quarterMaterial);
        // eighthMaterial = (eighthMaterial == null? (Material)Resources.Load("Red",typeof(Material)) : eighthMaterial);
        // if (eighthMaterial == null){
        //     eighthMaterial = (Material)Resources.Load("Red",typeof(Material));
        // }
        // sixteenthMaterial = (sixteenthMaterial == null? (Material)Resources.Load("Green",typeof(Material)) : sixteenthMaterial);

        incomingNoteBlocks = new List<NoteBlock>();
        topNotes = new List<NoteBlock>();
        nextNIndex = 0;
        nextN = song.notes[nextNIndex++];

        toolate_seconds = TOOLATE_TICKS/4f/timer.BPM*60f;
        okwindow_seconds = OKWINDOW_TICKS/4f/timer.BPM*60f;

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
        if(timer.sixteenth % 1 == 0 ){
            grabTopNotes();
            purgeFinishedNotes();
        }
        
    }

    //spawns LOADEDBARS # of bars of notes starting at the given tick
    // assumes notes are in ascending order of tick value
    void spawnNoteBlocks(int tick){
        Vector3 notePos;
        
        localTransformSansScale = new GameObject().transform;
        localTransformSansScale.position = this.transform.position;
        localTransformSansScale.rotation = this.transform.rotation;
        
        float dist;
        // float toChartTop = this.transform.localScale.y /2 + grid.GetTextureOffset("_MainTex").y;
        float toChartTop = this.transform.localScale.y /2f;
        float toChartSide = this.transform.localScale.x/2f;
        // float toChartSide = 0;
        
        NoteBlock curr;
        
        while (nextN.tick <= (tick + 16*(LOADEDBARS + 1)) && nextNIndex <= song.notes.Count ){
            dist = -( (nextN.tick-timer.sixteenth)*(toChartTop/16) );
            // print("nextN.tick: "+nextN.tick);
            // TODO can change x pos, and add types here
            // notePos = new Vector3(Random.Range(toChartSide, -toChartSide), dist + toChartTop, -1) + this.transform.position;
            notePos = new Vector3(nextN.xPos*toChartSide, dist + toChartTop, -1) + this.transform.position;
            // print("tick: "+n.tick);

            curr = Instantiate(noteBlockToSpawn, notePos, noRot, this.transform);
            curr.transform.parent = songTop.transform;
            curr.transform.localScale = oneScale;
            curr.note = nextN;
            if (nextN.isQuarter()){
                // curr.GetComponent<Renderer>().material = quarterMaterial;
                // curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",new Color(1, 0.2f, 0));
                // curr.GetComponent<Renderer>().material.SetColor("_AlbedoColor",new Color(1, 0.2f, 0));
                // curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",new Color(255, 255, 255));
                curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",quarterCol);
                curr.GetComponent<Renderer>().material.SetColor("_AlbedoColor",quarterCol);
                curr.GetComponent<Light>().color = quarterCol;
                
            }else if(nextN.isEighth()){
                // curr.GetComponent<MeshRenderer>().material = eighthMaterial;
                // curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.blue);
                // curr.GetComponent<Renderer>().material.SetColor("_AlbedoColor",Color.blue);
                // curr.transform.localScale  = new Vector3(2,2,2);
                curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",eighthCol);
                curr.GetComponent<Renderer>().material.SetColor("_AlbedoColor",eighthCol);
                curr.GetComponent<Light>().color = eighthCol;
            }else if(nextN.isSixteenth()){
                // curr.GetComponent<Renderer>().material = sixteenthMaterial;
                // curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.green);
                curr.GetComponent<Renderer>().material.SetColor("_EmissionColor",sixteenthCol);
                curr.GetComponent<Renderer>().material.SetColor("_AlbedoColor",sixteenthCol);
                curr.GetComponent<Light>().color = sixteenthCol;
            }
            incomingNoteBlocks.Add(curr);
            
            if (nextNIndex < song.notes.Count){
                nextN = song.notes[nextNIndex++];
            }else{
                nextNIndex++;
            }
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
        grid.SetTextureOffset("_MainTex",currOffset + new Vector2(0,y));
    }
    void scrollNotes(float y){
        // songTop.transform.Translate(0,y*this.transform.localScale.y/2,0,localTransformSansScale);
        songTop.transform.Translate(0,y*this.transform.localScale.y/2,0,this.transform);
    }

    public void correctScroll(int ticks){
        scrollAll(ticks*0.0625f); // 0.0625 = 1/16
        timer.correct(ticks);
    }
    public void correctScroll(float seconds){
        scrollAll(seconds * (timer.BPM/240));// BPM/240: bars per second
        timer.correct(seconds);
    }

    private int grabTopNotes(){
        int grabbed = 0;
        // topNote = 0;
        // does grabbing [0] and popping + pushing work?
        while ( incomingNoteBlocks.Count > 0 && incomingNoteBlocks[0].getCurrentDifference() < OKWINDOW_TICKS/4f/timer.BPM*60f){
            topNotes.Add(incomingNoteBlocks[0]);
            // incomingNoteBlocks[0].GetComponent<Renderer>().material = null;
            incomingNoteBlocks.RemoveAt(0);
            grabbed++;
        }
        
        return grabbed;
    }

    public bool noteIsHittable(int index){
        return topNotes.Count > 0 && topNotes[index].getCurrentDifference() < okwindow_seconds;
    }

    public int hit(KeyCode key = KeyCode.Z){
        float PERFECT_SECONDS = 0.07f;
        const int POINTSPERFECT = 10;

        float GOOD_SECONDS = 0.15f;
        const int POINTSGOOD = 7;

        const int POINTSOK = 5;

        const int POINTSMISS = -2;

        string accuracy;
        int points;
        // different x positions to be implemented here, change index 0 to match hit note
        if (noteIsHittable(index:0)){
            NoteBlock curr = topNotes[0];
            if(Mathf.Abs(curr.getCurrentDifference()) < PERFECT_SECONDS){
                points = POINTSPERFECT;
                accuracy = (curr.getCurrentDifference() < 0 ? "Great!-" : "-Great!");
                
            }else if(Mathf.Abs(curr.getCurrentDifference()) < GOOD_SECONDS){
                points = POINTSGOOD;
                accuracy = (curr.getCurrentDifference() < 0 ? "Good-" : "-Good");
            }else {
                points = POINTSOK;
                accuracy = (curr.getCurrentDifference() < 0 ? "OK-" : "-OK");
            }
            if (judgeXpos(key, curr, this.GetComponent<PlayerInputController>())){
                print("cool!");
            }
            curr.explode(accuracy);
            score += points;
        }else{
            score += POINTSMISS;
            accuracy = "Miss...";
        }
        slider.value = score;
        txt.GetComponent<UnityEngine.UI.Text>().text = score.ToString() + " "+accuracy;
        cheer.text = accuracy;
        // cheer.GetComponentInParent<Animator>().Play("bb");
        switch (distToNextNote())
        {   
            case 1:
                cheer.GetComponentInParent<Animator>().Play("bump16");
                break;
            case 2:
                cheer.GetComponentInParent<Animator>().Play("bump8");
                break;
            case 4:
            default:
                cheer.GetComponentInParent<Animator>().Play("bump4");
                break;
        }
        if (topNotes.Count > 0){
            topNotes.RemoveAt(0);
        }
        return -1;

    }
    
    private bool judgeXpos(KeyCode key, NoteBlock note, PlayerInputController pi){
        bool cool = false;
        int keyX = pi.keyToXpos(key);
        float xpos = note.transform.position.x;

        if(prevNoteX == xpos && prevKeyX == keyX){
            cool = true;
        }else if (prevNoteX < xpos && prevKeyX < keyX){
            cool = true;
        }else if (prevNoteX > xpos && prevKeyX > keyX){
            cool = true;
        }


        prevNoteX = xpos;
        prevKeyX = keyX;
        return cool;
    }

    public int distToNextNote(){
        if (topNotes.Count >= 2){
            return topNotes[1].note.tick - topNotes[0].note.tick;
        }
        else{
            return 16;
        }
    }

    //TODO!!!! don't scan through notes sequentially, here or above...
    private void purgeFinishedNotes(){
        // print("topnote: "+topNote+" currdiff "+noteBlocks[topNote].getCurrentDifference());
        // clear out the missed notes
        // print("-TOOLATE/4f/timer.BPM*60f: "+(-TOOLATE/4f/timer.BPM*60f));
        // print("topNotes[0].getCurrentDifference(): "+topNotes[0].getCurrentDifference());
        while (topNotes.Count > 0 && topNotes[0].getCurrentDifference() < -toolate_seconds){
            // print("purged " + topNotes[0].note.tick);
            topNotes[0].die(miss:true);
            topNotes.RemoveAt(0);
            score -= 10;
            slider.value = score;
        }
        return;
    }
}