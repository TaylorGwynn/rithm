/*
Takes player inputs and checks them against the current song.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public songTimer timer;
    public Chart chart;
    public Dictionary<KeyCode, int> keyXdict;

    // Start is called before the first frame update
    void Start()
    {
        keyXdict = new Dictionary<KeyCode, int>();
        List<KeyCode> row1 = new List<KeyCode>
        {
            KeyCode.Tilde, 
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0,
            KeyCode.Minus,
            KeyCode.Equals,
            KeyCode.Backspace,
            };
        List<KeyCode> row2 = new List<KeyCode>
        {
            KeyCode.Tab, 
            KeyCode.Q,
            KeyCode.W,
            KeyCode.E,
            KeyCode.R,
            KeyCode.T,
            KeyCode.Y,
            KeyCode.U,
            KeyCode.I,
            KeyCode.O,
            KeyCode.P,
            KeyCode.LeftBracket,
            KeyCode.RightBracket,
            KeyCode.Backslash,
            };
        List<KeyCode> row3 = new List<KeyCode>
        {
            KeyCode.CapsLock, 
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            KeyCode.Semicolon,
            KeyCode.Quote,
            KeyCode.Return,
            KeyCode.Delete //I got lazy here
            };
        List<KeyCode> row4 = new List<KeyCode>
        {
            KeyCode.LeftShift, 
            KeyCode.Z,
            KeyCode.X,
            KeyCode.C,
            KeyCode.V,
            KeyCode.B,
            KeyCode.N,
            KeyCode.M,
            KeyCode.Comma,
            KeyCode.Period,
            KeyCode.Slash,
            KeyCode.RightShift,
            KeyCode.RightControl,//I got lazy here
            KeyCode.LeftArrow//I got lazy here
            };
        for (int i = 0; i < row1.Count; i++)
        {
            keyXdict[row1[i]] = i;
            keyXdict[row2[i]] = i;
            keyXdict[row3[i]] = i;
            keyXdict[row4[i]] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // print(Input.inputString);
        if(Input.anyKeyDown){
            chart.hit(polledKeyPressed());
        }
    }

    // void OnGUI() {
    //     if (Event.current.isKey && Event.current.type == EventType.KeyDown){
    //         chart.hit(Event.current.keyCode);
    //     }
    // }

    // placeholder to test out notes being hit
    public int checkIfHit(){
        return 0;
    }

    public KeyCode polledKeyPressed(){
        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode))){
            if (Input.GetKeyDown(key)){
                return key;
            }
        }
        return KeyCode.Z;
    }

    public int keyToXpos(KeyCode key){
        int xpos = 0;
        try
        {
            xpos = this.keyXdict[key];
        }
        catch (System.Exception)
        {
            
            // throw;
        }
        return xpos;
    }
}
