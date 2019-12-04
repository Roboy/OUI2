using System.Collections.Generic;
using UnityEngine;

public class View<T> : ISubscriber<T>
{
    DisplayView display;

    public View(DisplayView display)
    {
        this.display = display;
    }

    public void DisplayData(Queue<T> data)
    {       
        T[] dataArray = data.ToArray();

        for (int i = 0; i < data.Count; i++)
        { 
            display.data[i] = dataArray[i].ToString();
        }
    }

    public void ReceiveUpdate(Queue<T> data)
    {
        DisplayData(data);
    }
}
