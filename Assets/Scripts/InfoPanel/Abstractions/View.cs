using System.Collections.Generic;
using UnityEngine;

public abstract class View<T> : ISubscriber<T>
{
    protected MonoBehaviour display;

    public View(MonoBehaviour display, Model<T> model)
    {
        this.display = display;
        SubscribeToModel(model);
    }

    public abstract void DisplayData(Queue<T> data);

    public void ReceiveUpdate(Queue<T> data)
    {
        DisplayData(data);
    }

    public void SubscribeToModel(Model<T> modelToSubscribeTo)
    {
        modelToSubscribeTo.Subscribe(this);
    }
}
