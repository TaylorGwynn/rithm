using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class util
{
    //returns a vector max len 2 randomly aligned on the xy plane
    public static Vector3 randomVector(){
        Vector3 randv;
        System.Random rand = new System.Random();
        randv = new Vector3((float)rand.NextDouble(), (float)rand.NextDouble(), 0);
        return randv;
    }
}
