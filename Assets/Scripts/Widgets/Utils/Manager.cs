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
                testToastrWidget.ForwardRosMessage(testToastrWidget.GetContext());
            }
        }

        private void ForwardMessageToWidget(RosJsonMessage rosMessage)
        {
            Widget widget = FindWidgetWithID(rosMessage.id);

            if (widget == null)
            {
                Debug.Log("Message with no matching widget id: " + rosMessage.id + " received.");
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
}