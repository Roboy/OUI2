using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphWidget : Widget
    {
        public readonly int SIZE = 10;

        public Color color = Color.white;

        public List<float> datapoints;
        public float[] datapointsArray;
        
        public void Awake()
        {
            datapoints = new List<float>();
        }

        public new void Init(RosJsonMessage context)
        {
            color = WidgetUtility.BytesToColor(context.graphColor);

            base.Init(context);
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
                datapoints.Remove(0);
            }

            datapoints.Add(newDatapoint);

            datapointsArray = datapoints.ToArray();

            if (view != null)
            {
                ((GraphView)view).UpdateView(this);
            }
        }

        protected override void UpdateInClass()
        {

        }

        public override View AddViewComponent(GameObject viewGameObject)
        {
            return viewGameObject.AddComponent<GraphView>();
        }

        public struct Datapoint
        {
            float time;
            float data;
        }
    }
}