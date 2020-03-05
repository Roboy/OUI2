using System;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphWidget : Widget
    {
        public readonly int SIZE = 100;

        public string unit;
        public Color color = Color.white;

        public int numXLabels = 2;
        public int numYLabels = 3;

        public bool showCompleteHistory;


        public List<Datapoint> datapoints;
        
        public void Awake()
        {
            datapoints = new List<Datapoint>();
        }

        public new void Init(RosJsonMessage context)
        {
            unit = context.graphTitle;
            color = WidgetUtility.BytesToColor(context.graphColor);
            numXLabels = context.xDivisionUnits;
            numYLabels = context.yDivisionUnits;

            base.Init(context);
        }

        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            if (rosMessage.graphColor != null && rosMessage.graphColor.Length == 4)
            {
                color = WidgetUtility.BytesToColor(rosMessage.graphColor);
            }
            DateTime dt = DateTime.Now;
            if (rosMessage.graphTimestamp != 0)
            {
                System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                dt = epochStart.AddSeconds(rosMessage.graphTimestamp);
            }
            if (rosMessage.graphValue != 0)
            {
                AddDatapoint(new Datapoint(dt, rosMessage.graphValue));
            }
        }

        public void AddDatapoint(Datapoint newDatapoint)
        {
            if (datapoints.Count == SIZE)
            {
                datapoints.RemoveAt(0);
            }

            datapoints.Add(newDatapoint);

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
            public DateTime time;
            public float data;

            public Datapoint(DateTime time, float newDatapoint)
            {
                this.time = time;
                this.data = newDatapoint;
            }
        }
    }
}