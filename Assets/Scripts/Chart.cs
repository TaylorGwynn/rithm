using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : MonoBehaviour
{
    public songTimer timer;
    public songLoader songLoader;
    protected Song song ;

    public Material quarterMaterial;
    public Material eighthMaterial;
    public Material sixteenthMaterial;
    // Start is called before the first frame update
    void Start()
    {
        //TODO make songPlayer superclass?
        if (timer == null){
            timer = (songTimer)GameObject.Find("songTimer").GetComponent(typeof(songTimer));
        }
        if (songLoader == null){
            print("No song detected for "+this.gameObject.name+", using placeholder song.");
            song = new Song("sixteenth","1   1 111 1111 11 ");
        }else{
            song = songLoader.song;
        }
        quarterMaterial = (quarterMaterial == null? (Material)Resources.Load("Assets/materials/Blue",typeof(Material)) : quarterMaterial);
        eighthMaterial = (eighthMaterial == null? (Material)Resources.Load("Red",typeof(Material)) : eighthMaterial);
        if (eighthMaterial == null){
            eighthMaterial = (Material)Resources.Load("Red",typeof(Material));
        }
        sixteenthMaterial = (sixteenthMaterial == null? (Material)Resources.Load("Green",typeof(Material)) : sixteenthMaterial);

        spawnNotes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnNotes(){
        Vector3 notePos;
        float offset;
        Quaternion rot = new Quaternion(0,0,0,0);
        GameObject curr;
        foreach (Note n in song.notes){
            offset = -(n.tick*0.25f);
            // TODO can change x pos, and add types here
            notePos = new Vector3(0, offset+2, -1) + this.transform.position;
            print("tick: "+n.tick);
            // TODO optimize by only creating this bar + next... put in Update()...
            curr = Instantiate(GameObject.Find("cone"), notePos, rot);
            curr.transform.parent = this.transform;
            if (n.isQuarter()){
                curr.GetComponent<Renderer>().material = quarterMaterial;
            }else if(n.isEighth()){
                curr.GetComponent<MeshRenderer>().material = eighthMaterial;
            }else if(n.isSixteenth()){
                curr.GetComponent<Renderer>().material = sixteenthMaterial;
            }
        }
    }

}
