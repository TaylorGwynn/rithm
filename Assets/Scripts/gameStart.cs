using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStart : MonoBehaviour
{
    public songTimer st;
    public audioController ac;
    // Start is called before the first frame update
    void Start()
    {
        st.pause();
        StartCoroutine("pauseForStartup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator pauseForStartup()
    {
        print("uh");
        yield return new WaitForSeconds(0.1f);
        print("WAITED");
        ac.begin();
        st.restart();
        st.correct(-5);

    }

}
