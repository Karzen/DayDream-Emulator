using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmulatorCore : MonoBehaviour
{
    public EmulatorData data = null;
    public Texture2D videoWrite = null;
    public RawImage screenDisplay = null;
    public RectTransform screenSize;

    void Start()
    {
        screenSize.sizeDelta = new Vector2(data.resolutionX, data.resolutionY);
        Application.targetFrameRate = data.targetFrameRate;
        Screen.SetResolution(data.resolutionX, data.resolutionY , false);
        videoWrite = new Texture2D(data.resolutionX, data.resolutionY);
        screenDisplay.material.mainTexture = videoWrite;
    }

    void Update()
    {
        render();
    }

    Color solveColor(short index)
    {
        switch ((int)data.vRam[index])
        {
            case 1:
                return Color.red;       
            case 2:
                return Color.blue;       
            case 3:
                return Color.green;        
            case 4:
                return Color.yellow;
            case 5:
                return Color.black;
            default:
                return Color.white;
        }
    }

    private void render() {
        Debug.Log(data.vRam.Length);
        for(short i = 0; i < data.vRam.Length; i++)
        {
            writePixel(i);
        }
        videoWrite.Apply();
    }

    void writePixel(short location)
    {

        int row = location / data.resolutionX;
        int col = location - row*data.resolutionX;

        if (location % data.resolutionX == 0)
        {
            row -= 1;
        }
        col = location - (row * data.resolutionX + 1) ;

        videoWrite.SetPixel(col, row, solveColor(location));
    }

}
