using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReceivedJsonData
{
    public int template_ID;
    public string type;
    public byte[] data;
    public int pos;
    
    // determines how many labels should be shown on the x axis of the graph
    public int numLabelsShownX;
    // determines how many labels should be shown on the y axis of the graph
    public int numLabelsShownY;
    // the color of the graph
    public int color;

    public ReceivedJsonData(int template_ID, string type, byte[] data, int pos, int numLabelsShownX, int numLabelsShownY, int color)
    {
        this.template_ID = template_ID;
        this.type = type;
        this.data = data;
        this.pos = pos;
        this.numLabelsShownX = numLabelsShownX;
        this.numLabelsShownY = numLabelsShownY;
        this.color = color;
    }
}
