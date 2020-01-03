using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphModel : Model
{
    const int SIZE = 100;

    public int detailedPanelPos;

    public int color;

    Queue<float> datapoints;

    public GraphModel(View view, int pos, string title, int detailedPanelPos, int color) : base(view, pos, title)
    {
        this.color = color;
        this.detailedPanelPos = detailedPanelPos;
        datapoints = new Queue<float>(SIZE);
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

        datapoints.Enqueue(newGraphMessage.datapoint);

        view.UpdateView(this);
    }
}
