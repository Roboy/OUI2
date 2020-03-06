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
            rosSubscriber = GameObject.FindGameObjectWithTag("RosManager").GetComponent<RosSubscriber>();
            widgets = Factory.Instance.CreateWidgetsAtStartup();
        }

        void Update()
        {
            if (!rosSubscriber.IsEmpty())
            {
                ForwardMessageToWidget(rosSubscriber.DequeueInterfaceMessage());
            }

            // Mock Area
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
                Widget iconTestWidget = FindWidgetWithID(29);
                iconTestWidget.GetContext().currentIcon = "RedWine";
                iconTestWidget.ProcessRosMessage(iconTestWidget.GetContext());
            }
        }

        private void ForwardMessageToWidget(RosJsonMessage rosMessage)
        {
            if (rosMessage == null)
            {
                Debug.LogWarning("RosMessage was null");
                return;
            }

            Widget widget = FindWidgetWithID(rosMessage.id);

            if (widget == null)
            {
                Debug.LogWarning("Message could not be forwarded.");
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

            Debug.LogWarning("No widget with id " + id + " found.");
            return null;
        }
    }
}