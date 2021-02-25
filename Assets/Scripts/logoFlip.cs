using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logoFlip : MonoBehaviour
{
    public int SPEED = 4;
    public float timer = 0.0f;
    List<string> logos;
    int logoInd;
    public UnityEngine.UI.Text txt;
    // Start is called before the first frame update
    void Start()
    {
        logoInd = 0;
        logos = new List<string>( new string[] 
        {
            "/KeyJam",
            "-KeyJam",
            "\\KeyJam",
            "|KeyJam"
        });

        // StartCoroutine("Flip");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        logoInd =  Mathf.FloorToInt(timer*SPEED);
        if (logoInd > 3){
            timer = 0;
            logoInd = 0;
        }
        txt.text = logos[(logoInd)];
        print(logoInd);
    }

    IEnumerator Flip(){
        // print(logoInd);
        txt.text = logos[(logoInd)];
        logoInd++;
        if (logoInd >= logos.Count){
            logoInd = 0;
        }
        yield return new WaitForSeconds(SPEED);
        yield return StartCoroutine("Flip");
    }

}
