using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView<T> : View<T> where T : DataPoint<float>
{
    public GraphView(GraphDisplay display, Model<T> model) : base(display, model)    
    {
    }

    public override void DisplayData(Queue<T> data)
    {
        ((GraphDisplay)display).AddDataPoint(data.Peek().GetValue());
    }
}
