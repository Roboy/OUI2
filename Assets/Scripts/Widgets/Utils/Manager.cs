using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class Manager : Singleton<Manager>
    {
        public List<Widget> widgets;
        public RosSubscriber rosSubscriber;

        void Start()
        {
            rosSubscriber = GameObject.FindGameObjectWithTag("ROS_Manager").GetComponent<RosSubscriber>();
            widgets = Factory.Instance.CreateWidgetsAtStartup();
        }

        void Update()
        {
            if (!rosSubscriber.IsEmpty())
            {
                ForwardMessageToWidget(rosSubscriber.DequeueInterfaceMessage());
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Widget testToastrWidget = FindWidgetWithID(10);
                testToastrWidget.ProcessRosMessage(testToastrWidget.GetContext());
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Widget testGraphWidget = FindWidgetWithID(1);
                testGraphWidget.ProcessRosMessage(testGraphWidget.GetContext());
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Widget iconTestWidget = FindWidgetWithID(20);
                iconTestWidget.GetContext().currentIcon = "SenseGlove_1";
                iconTestWidget.ProcessRosMessage(iconTestWidget.GetContext());
            }
        }

        private void ForwardMessageToWidget(RosJsonMessage rosMessage)
        {
            Widget widget = FindWidgetWithID(rosMessage.id);

            if (widget == null)
            {
                Debug.Log("Message could not be forwarded.");
                return;
            }

            widget.ProcessRosMessage(rosMessage);
        }

        public Widget FindWidgetWithID(int id)
        {
            foreach (Widget widget in widgets)
            {
                if (widget.GetID() == id)
                {
                    return widget;
                }
            }

            Debug.Log("No widget with id " + id + " found.");
            return null;
        }
    }
}