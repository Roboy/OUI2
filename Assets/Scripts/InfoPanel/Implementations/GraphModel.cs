using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphModel : Model
{
    public const int SIZE = 10;

    public int detailedPanelPos;

    public int color;

    public Queue<float> datapoints;

    public GraphModel(View view, int pos, string title, int detailedPanelPos, int color) : base(view, pos, title)
    {
        this.color = color;
        this.detailedPanelPos = detailedPanelPos;
        datapoints = new Queue<float>(SIZE);
    }

    public static explicit operator GraphModel(int v)
    {
        throw new NotImplementedException();
    }

    public override void UpdateModel(WidgetMessage newMessage)
    {
        GraphMessage newGraphMessage = (GraphMessage)newMessage;

        if (newGraphMessage.color != 0)
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
    }
}
