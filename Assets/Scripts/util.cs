/*
Utility functions
*/

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

    public static Vector3 divideScales(Vector3 a, Vector3 b){
        return new Vector3(a.x/b.x, a.y/b.y, a.z/b.z);
    }

    // public static bool initSongComponent(Component ownComponent, System.Type type){
    //     // System.Type type = ownComponent.GetType();
    //     if (ownComponent == null){
    //         ownComponent = GameObject.Find("SongSource").GetComponent(type);
    //     }if (ownComponent == null){
    //         ownComponent = GameObject.FindGameObjectWithTag("SongSource").GetComponent(type);
    //     }if (ownComponent == null){
    //         foreach (GameObject item in GameObject.FindObjectsOfType<GameObject>())
    //         {
    //             if(item.GetComponent(type) != null){
    //                 ownComponent = item.GetComponent(type);
    //                 return true;
    //             }
    //         }
    //     }
    //     if (ownComponent != null){
    //         return true;
    //     }else{
    //         return false;
    //     }
    // }
}
