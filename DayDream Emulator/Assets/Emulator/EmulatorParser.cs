using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulatorParser : MonoBehaviour
{

    public string[] assembly;
    public string[] splitInstruction;

    public EmulatorData data;

    private string instruction;

    public char[] reg;


    private bool isRegistry(string data)
    {
        if(data[0] == 'r')
            return true;
        return false;
    }

    private ref char solveRegistry(string instruction)
    {
        if (instruction.Length > 3)
        {
            instruction = instruction.Substring(0, 3);
        }
        switch (instruction) {
            case "rg1":
                return ref data.rg1;                
            case "rg2":
                return ref data.rg2;                
            case "rg3":
                return ref data.rg3;                
            case "rg4":
                return ref data.rg4;              
            case "rg5":
                return ref data.rg5;                
            case "rg6":
                return ref data.rg6;
            case "rsc":
                return ref data.rsc;
            case "rsn":
                return ref data.rsn;
            default:
                return ref data.rg6;
        }
    }
    private short decodeInt(string data)
    {
        return (short)Int32.Parse(data);
    }

    private char solveValue(string data)
    {
        if (isRegistry(data))
        {
            return solveRegistry(data);
        }
        else
        {
            return (char)decodeInt(data);
        }
    }

    void addToMem(string address, string datt)
    {
        int addr = Int32.Parse(address);
        int dat = solveValue(datt);

        if(addr  <= 128)
        {
            data.ram[addr] = (char)dat;
        }

    }
    void loadFromMem(string address, string reg)
    {
        int addr = Int32.Parse(address);
        solveRegistry(reg) = data.ram[addr];
    }

    void runF(string arg)
    {
        if (arg.Length > 3)
        {
            arg = arg.Substring(0, 3);
        }
        if(arg == "sdt")
        {
            Debug.Log("sdat");
            Debug.Log((int)data.rsc);
        }
    }

    void Start()
    {
        assembly = (System.IO.File.ReadAllText("D:\\Unity\\loaded_game.dda")).Split('\n');
        
    }

    void haide()
    {
        for (short currentAdr = 0; currentAdr < assembly.Length; currentAdr++)
        {
            data.rsi = currentAdr;
            splitInstruction = assembly[currentAdr].Split(' ');
            instruction = splitInstruction[0];
            switch (instruction)
            {
                case "mut":
                    solveRegistry(splitInstruction[1]) = solveValue(splitInstruction[2]);      
                    break;
                case "adu":
                    solveRegistry(splitInstruction[1]) = (char)(solveValue(splitInstruction[2]) + solveRegistry(splitInstruction[1]));
                    break;
                case "sar":
                    currentAdr = (short)(decodeInt(splitInstruction[1])-1);
                    break;
                case "sca":
                    solveRegistry(splitInstruction[1]) = (char)(solveRegistry(splitInstruction[1]) - solveValue(splitInstruction[2]));
                    break;
                case "sto":
                    addToMem(splitInstruction[2], splitInstruction[1]);
                    break;
                case "inc":
                    loadFromMem(splitInstruction[2], splitInstruction[1]);
                    break;
                case "srd":
                    if(solveRegistry(splitInstruction[1]) != 0)
                        currentAdr = (short)(Int16.Parse(splitInstruction[2])-1);
                    break;
                case "inv":
                    if (solveRegistry(splitInstruction[1]) == (char)0)
                        solveRegistry(splitInstruction[1]) = (char)1;
                    else if(solveRegistry(splitInstruction[1]) == (char)1)
                        solveRegistry(splitInstruction[1]) = (char)0;
                    break;
                case "com":
                   if(solveRegistry(splitInstruction[1]) > solveValue(splitInstruction[2]))
                    {
                        solveRegistry(splitInstruction[1]) = (char)1;
                        break;
                    }
                    else
                    {
                        solveRegistry(splitInstruction[1]) = (char)0;
                        break;
                    }

                case "ega":
                    if (solveRegistry(splitInstruction[1]) == solveValue(splitInstruction[2]))
                        solveRegistry(splitInstruction[1]) = (char)1;
                    else
                        solveRegistry(splitInstruction[1]) = (char)0;
                    break;
                case "vrm":
                    Debug.Log("yayay");
                   Debug.Log((int)solveValue(splitInstruction[1]) + " " + (int)solveValue(splitInstruction[2]));
                   data.vRam[solveValue(splitInstruction[1]) + (solveValue(splitInstruction[2]) * data.resolutionX) ] = (char)data.rsc;
                   break;

                case "rul":
                    runF(splitInstruction[1]);
                    break;
            }
        }
    }
    void Update()
    {
        haide();
    }
}
