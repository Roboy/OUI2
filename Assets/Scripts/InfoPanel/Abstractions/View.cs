using System.Collections.Generic;
using UnityEngine;

public abstract class View<T> : MonoBehaviour, ISubscriber<T>
{
    public abstract void DisplayData(Queue<T> data);

    public void ReceiveUpdate(Queue<T> data)
    {
        DisplayData(data);
    }
}
