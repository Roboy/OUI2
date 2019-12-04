using System.Collections.Generic;

public class InspectorView<T, U> : View<T> where T : DataPoint<U>
{
    public InspectorView(InspectorDisplay display, Model<T> model) : base(display, model)
    {        
    }

    public override void DisplayData(Queue<T> data)
    {
        T[] dataArray = data.ToArray();

        for (int i = 0; i < data.Count; i++)
        {
            ((InspectorDisplay)display).data[i] = dataArray[i].GetTimestamp().ToString();
        }
    }
}

