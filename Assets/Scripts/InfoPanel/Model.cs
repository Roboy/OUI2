using System.Collections.Generic;
using UnityEngine;

public class Model<T>
{
    int size;
    Queue<T> data;

    public void InitializeModel (int size)
    {
        this.size = size;
        data = new Queue<T>(size);
    }

    public void AddDataPoint(T dataPoint)
    {
        data.Enqueue(dataPoint);

        if (data.Count > size)
            data.Dequeue();
    }

    public Queue<T> GetData()
    {
        return data;
    }
}
