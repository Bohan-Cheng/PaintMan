using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum EPaintColor
{
    BLUE,
    GREEN,
    RED,
    YELLOW,
    DEFUALT
}

[Serializable]
struct ColorMap
{
    public EPaintColor colorType;
    public Material colorMat;
}

public class PaintDrop : MonoBehaviour
{
    [SerializeField] bool RandomColor = true;
    [SerializeField] EPaintColor paintColor = EPaintColor.DEFUALT;
    [SerializeField] ColorMap[] colorMap;
    // Start is called before the first frame update
    void Start()
    {
        if(RandomColor)
        {
            paintColor = (EPaintColor)UnityEngine.Random.Range(0, (int)EPaintColor.DEFUALT);
        }
        GetComponent<Renderer>().material = FindMat(paintColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Material FindMat(EPaintColor color)
    {
        foreach (ColorMap c in colorMap)
        {
            if(c.colorType == color)
            {
                return c.colorMat;
            }
        }
        return null;
    }
}
