using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : Singleton<WidgetManager>
{
    public List<Widget> widgets;
    public InterfaceMessageSubscriber interfaceMessageSubscriber;

    void Start()
    {
        interfaceMessageSubscriber = GameObject.FindGameObjectWithTag("ROS_Manager").GetComponent<InterfaceMessageSubscriber>();
        widgets = WidgetFactory.Instance.CreateWidgetsAtStartup();
    }

    void Update()
    {
        if (!interfaceMessageSubscriber.IsEmpty())
        {
            ForwardMessageToWidget(interfaceMessageSubscriber.DequeueInterfaceMessage());
        }
    }

    private void ForwardMessageToWidget(JSON_message rosMessage)
    {
        Widget widget = FindWidgetWithID(rosMessage.id);

        if (widget == null)
        {
            Debug.LogWarning("Message with no matching widget id: " + rosMessage.id + " received.");
            return;
        }

        widget.ForwardRosMessage(rosMessage);   

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
