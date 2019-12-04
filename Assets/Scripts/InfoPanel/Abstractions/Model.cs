﻿using System.Collections.Generic;
using UnityEngine;

public class Model<T> : IPublisher<T> 
{
    int size;
    Queue<T> data;

    List<ISubscriber<T>> subscriberList;

    public Model(int size)
    {
        this.size = size;
        data = new Queue<T>(size);
    }

    public void AddDataPoint(T dataPoint)
    {
        data.Enqueue(dataPoint);
        
        if (data.Count > size)
        {
            data.Dequeue();
        }

        Notify();
    }

    public Queue<T> GetData()
    {
        return data;
    }

    #region Publisher Interface

    public void Subscribe(ISubscriber<T> subscriber)
    {
        if (subscriberList == null)
            subscriberList = new List<ISubscriber<T>>();

        subscriberList.Add(subscriber);
    }

    public void Notify()
    {
        foreach(ISubscriber<T> subscriber in subscriberList)
        {
            subscriber.ReceiveUpdate(data);
        }
    }

    public void Unsubscribe(ISubscriber<T> subscriber)
    {
        if (subscriberList == null)
            return;

        subscriberList.Remove(subscriber);
    }

    #endregion
}
