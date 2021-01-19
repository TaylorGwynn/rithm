using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class jsonTest : MonoBehaviour
{
    public Song testsong;
    // Start is called before the first frame update
    void Start()
    {
        testsong = new Song("eighth","1 1 111 1111 11 ");
        // print(JsonUtility.ToJson(testsong));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
