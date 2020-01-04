using System;
using UnityEngine;

[Serializable]
public class WidgetContext
{
    public int template_ID;
    public string type;
    public string title;
    public int pos;

    // determines where the DetailedPanel should be shown
    public int detailedPanelPos;
    // determines how many labels should be shown on the x axis of the graph
    public int numLabelsShownX;
    // determines how many labels should be shown on the y axis of the graph
    public int numLabelsShownY;
    // the color of the graph
    public int color;

    // the time how long this widget should be shown
    public float duration;

    public byte fontSize;
}