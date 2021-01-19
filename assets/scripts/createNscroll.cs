using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createNscroll : MonoBehaviour
{
    public GameObject thing;
    public List<GameObject> things;
    // Start is called before the first frame update
    void Start()
    {
        float dt = Time.deltaTime;
        
        things.Add(Instantiate(thing, this.transform.position, this.transform.rotation));
        things.Add(Instantiate(thing, this.transform.position, this.transform.rotation));
        this.transform.Translate(0,0,dt);
    }

    // Update is called once per frame
    void Update()
    {   
        float dt = Time.deltaTime;
        this.transform.Translate(0,dt,dt);
        foreach (GameObject n in things)
        {   
            n.transform.Translate(dt/9, dt/9, 1);
        }
    }
}
