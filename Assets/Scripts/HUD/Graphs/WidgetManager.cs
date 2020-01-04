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

    void Update()
    {
        if (RosMock.Instance.HasNewMessage())
        {
            ForwardMessageToWidget(RosMock.Instance.DequeueMessage());
        }
    }

    private void ForwardMessageToWidget(RosMessage rosMessage)
    {
        Widget widget = FindWidgetWithID(rosMessage.id);

        if (widget == null)
        {
            Debug.LogWarning("Message with no matching widget id: " + rosMessage.id + " received.");
            return;
        }

        widget.ForwardRosMessage(rosMessage.data);   

    }

    private Widget FindWidgetWithID(int id)
    {
        foreach (Widget widget in widgets)
        {
            if (widget.GetID() == id)
            {                
                return widget;
            }
        }

        return null;
    }
}
