using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : Singleton<WidgetManager>
{
    public List<Widget> widgets;

    void Start()
    {
        widgets = WidgetFactory.Instance.CreateWidgetsAtStartup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
