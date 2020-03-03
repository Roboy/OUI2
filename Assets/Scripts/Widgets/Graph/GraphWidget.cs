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

        GraphView view;

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

        public override void RestoreViews(GameObject viewParent)
        {
            GameObject graphGameObject = new GameObject();
            graphGameObject.transform.SetParent(viewParent.transform, false);
            graphGameObject.name = gameObject.name + "View";
            view = graphGameObject.AddComponent<GraphView>();
            view.Init(datapoints, childWidget != null ? childWidget : null);
            
            if (isChildWidget)
            {
                view.HideView();
            }
        }

        public void Update()
        {
            // This is true, everytime the HUD scene changes
            if (view == null)
            {
                GameObject graphParent = GameObject.FindGameObjectWithTag("Panel_" + GetPanelID());

                if (graphParent != null)
                {
                    RestoreViews(graphParent);
                }
            }
        }

        protected override void UpdateInClass()
        {
            throw new System.NotImplementedException();
        }

        public override View GetView()
        {
            return view;
        }

        public struct Datapoint
        {
            float time;
            float data;
        }
    }
}