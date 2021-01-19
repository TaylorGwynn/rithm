using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createNscroll : MonoBehaviour
{
    public GameObject note;
    public List<GameObject> notes;
    // Start is called before the first frame update
    void Start()
    {
        float dt = Time.deltaTime;
        
        notes.Add(Instantiate(note, this.transform.position, this.transform.rotation));
        notes.Add(Instantiate(note, this.transform.position, this.transform.rotation));
        this.transform.Translate(0,0,dt);
    }

    // Update is called once per frame
    void Update()
    {   
        float dt = Time.deltaTime;
        this.transform.Translate(0,dt,dt);
        foreach (GameObject n in notes)
        {   
            n.transform.Translate(5f*dt, 5f*dt, 1);
        }
    }
}
