using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphModel : MonoBehaviour
{
    /*
    public const int SIZE = 10;

    public int detailedPanelPos;

    public Color color;

    public string iconName;

    public string thumbnailText;

    public Queue<float> datapoints;

    public GraphModel(View view, int pos, string title, int detailedPanelPos, Color color, string iconName, string thumbnailText) : base(view, pos, title)
    {
        this.color = color;
        this.detailedPanelPos = detailedPanelPos;
        this.iconName = iconName;
        this.thumbnailText = thumbnailText;
        datapoints = new Queue<float>(SIZE);
    }

    public static explicit operator GraphModel(int v)
    {
        throw new NotImplementedException();
    }

    public override void UpdateModel(WidgetMessage newMessage)
    {
        GraphMessage newGraphMessage = (GraphMessage)newMessage;

        if (!newGraphMessage.color.Equals(new Color(0, 0, 0, 0)))
        {
            color = newGraphMessage.color;
        }

        if (newGraphMessage.pos != 0)
        {
            pos = newGraphMessage.pos;
        }

        if (newGraphMessage.detailedPanelPos != 0)
        {
            detailedPanelPos = newGraphMessage.pos;
        }

        if (datapoints.Count == SIZE)
        {
            datapoints.Dequeue();
        }

        datapoints.Enqueue(newGraphMessage.datapoint);

        view.UpdateView(this);
    }*/
}
