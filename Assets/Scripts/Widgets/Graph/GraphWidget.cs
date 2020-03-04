﻿using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphWidget : Widget
    {
        public readonly int SIZE = 10;

        public Color color = Color.white;

        public List<float> datapoints;
        public float[] datapointsArray;

        GraphView view;

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
                view.UpdateView(this);
            }
        }

        public override void CreateView(GameObject viewParent)
        {
            GameObject graphGameObject = new GameObject();
            graphGameObject.transform.SetParent(viewParent.transform, false);
            graphGameObject.name = gameObject.name + "View";
            view = graphGameObject.AddComponent<GraphView>();
            view.Init(datapoints, childWidget != null ? childWidget : null, name, color);
            
            if (isChildWidget)
            {
                view.HideView();
            }
        }

        protected override void UpdateInClass()
        {
            // This is true, everytime the HUD scene changes
            if (view == null)
            {
                GameObject graphParent = GameObject.FindGameObjectWithTag("Panel_" + GetPanelID());

                if (graphParent != null)
                {
                    CreateView(graphParent);
                }
            }
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