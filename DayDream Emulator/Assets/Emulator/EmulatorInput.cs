using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulatorInput : MonoBehaviour
{
    public EmulatorData data;

    void Update()
    {
        
        if (Input.GetKeyDown("a"))
        {
            data.rsn = (char)2;
        }
        else if (Input.GetKeyDown("d"))
        {
            data.rsn = (char)1;
        }
        else if (Input.GetKeyDown("s"))
        {
            data.rsn = (char)3;
        }
        else if (Input.GetKeyDown("w"))
        {
            data.rsn = (char)4;
        }
        else if (Input.GetKeyUp("a"))
        {
            data.rsn = (char)0;
        }
        else if (Input.GetKeyUp("d"))
        {
            data.rsn = (char)0;
        }
        else if (Input.GetKeyUp("w"))
        {
            data.rsn = (char)0;
        }
        else if (Input.GetKeyUp("s"))
        {
            data.rsn = (char)0;
        }
        else if (Input.GetKeyDown("space"))
        {
            data.rsn = (char)5;
        }
        else if (Input.GetKeyUp("space"))
        {
            data.rsn = (char)0;
        }
    }
}
