using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulatorData : MonoBehaviour
{
    public short targetFrameRate = 60;
    public short resolutionX = 40;
    public short resolutionY = 192;

    
    public char[] vRam  = new char[7680];
    [HideInInspector]
    public char[] ram = new char[128];

    [HideInInspector]
    public int usedRam = 0;

    public Stack<char>  stack = new Stack<char>();

    //Registrii generali
    public char rg1, rg2, rg3, rg4, rg5, rg6;

    //Registrii speciali
    public char rsc, rsn=(char)0;
    public short rsi;


    void Start()
    {
        Debug.Log((int)rg6);
    }

 
}
