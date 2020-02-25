using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphModel : Model
    {
        public const int SIZE = 10;

        public Color color;

        public Queue<float> datapoints;

        public GraphModel(View view, string title, int panel_id, Color color) : base(view, title, panel_id)
        {
            this.color = color;
            datapoints = new Queue<float>(SIZE);
        }

        public void ChangeColor(Color color)
        {
            this.color = color;
        }

        public void AddDatapoint(float newDatapoint)
        {
            if (datapoints.Count == SIZE)
            {
                datapoints.Dequeue();
            }

            datapoints.Enqueue(newDatapoint);
        }
    }
}