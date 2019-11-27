using System.Collections.Generic;
using UnityEngine;

public class View<T>
{
    public void UpdateView(Queue<T> data)
    {
        Debug.Log(data.Peek());
    }
}
