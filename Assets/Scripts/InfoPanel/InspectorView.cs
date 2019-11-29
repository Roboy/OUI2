using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorView<T> : View<T>
{
    [SerializeField] private string[] displayedData;

    public override void DisplayData(Queue<T> data)
    {
        displayedData = new string[data.Count];

        int i = 0;

        while(data.Count != 0)
        {
            displayedData[i] = data.Dequeue().ToString();

            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        print("Inpsectorview started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
