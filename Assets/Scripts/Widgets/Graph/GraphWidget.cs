using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphWidget : Widget
    {
        public readonly int SIZE = 10;

        public Color color = Color.white;

        public int numXLabels = 2;
        public int numYLabels = 3;

        public List<float> datapoints;
        public float[] datapointsArray;
        
        public void Awake()
        {
            datapoints = new List<float>();
        }

        public new void Init(RosJsonMessage context)
        {
            color = WidgetUtility.BytesToColor(context.graphColor);
            numXLabels = context.xDivisionUnits;
            numYLabels = context.yDivisionUnits;

            base.Init(context);
        }

        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            if (rosMessage.graphColor != null || rosMessage.graphColor.Length < 4)
            {
                color = WidgetUtility.BytesToColor(rosMessage.graphColor);
            }
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