using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : Singleton<WidgetManager>
{
    public List<Widget> widgets;
    public WidgetRosSubscriber rosSubscriber;

    void Start()
    {
        rosSubscriber = GameObject.FindGameObjectWithTag("ROS_Manager").GetComponent<WidgetRosSubscriber>();
        widgets = WidgetFactory.Instance.CreateWidgetsAtStartup();
    }

    void Update()
    {
        if (!rosSubscriber.IsEmpty())
        {
            ForwardMessageToWidget(rosSubscriber.DequeueInterfaceMessage());
        }
    }

    private void ForwardMessageToWidget(RosJsonMessage rosMessage)
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
