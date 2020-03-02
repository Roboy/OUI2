using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphWidget : Widget
    {
        public readonly int SIZE = 10;

        public Color color;

        public Queue<float> datapoints;
        public float[] datapointsArray;

        public void Awake()
        {
            datapoints = new Queue<float>();
        }

        public new void Init(RosJsonMessage rosJsonMessage)
        {
            color = WidgetUtility.BytesToColor(rosJsonMessage.graphColor);

            base.Init(rosJsonMessage);
        }

        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            if (rosMessage.graphDatapoint != 0)
            {
                AddDatapoint(rosMessage.graphDatapoint);
            }
        }

        public void AddDatapoint(float newDatapoint)
        {
            if (datapoints.Count == SIZE)
            {
                datapoints.Dequeue();
            }

            datapoints.Enqueue(newDatapoint);

            datapointsArray = datapoints.ToArray();
        }
    }
}