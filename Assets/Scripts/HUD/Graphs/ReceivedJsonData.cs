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

    public ReceivedJsonData(int templateId, string type, byte[] data, int pos)
    {
        template_ID = templateId;
        this.type = type;
        this.data = data;
        this.pos = pos;
    }
}
