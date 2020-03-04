using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using ChartAndGraph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Widgets
{
    public class GraphManager : MonoBehaviour
    {
        public Material lineMaterial;

        private GraphChart graph;
        private VerticalAxis verticalAxis;
        private HorizontalAxis horizontalAxis;

        private int TotalPoints = 5;
        float lastTime = 0f;
        float lastX = 0f;

        private readonly string success = "s";

        public GraphManager(GraphChart graph)
        {
            this.graph = graph;
        }

        public void Init(string topic)
        {
            graph = GetComponent<GraphChart>();
            if (graph == null)
            {
                // the ChartGraph info is obtained via the inspector
                Debug.LogWarning("No GraphChart found! Place this script into a graph chart!");
                return;
            }

            lineMaterial = new Material(Shader.Find("Chart/Canvas/Solid"));
            graph.DataSource.AddCategory(topic, lineMaterial, 20, new MaterialTiling(false, 20), null, true, null, 20);

            graph.AutoScrollHorizontally = true;
            graph.DataSource.AutomaticHorizontalView = false;
            if (verticalAxis == null)
            {
                verticalAxis = graph.GetComponent<VerticalAxis>();
                horizontalAxis = graph.GetComponent<HorizontalAxis>();
                horizontalAxis.Format = AxisFormat.Time;
            }

            //graph.DataSource.AddCategory(topic, new Material(Shader.Find("Standard")), 20, new MaterialTiling(false, 20), null, true, null, 20);
            //CustomizeGraph("");
            //SetNumLabelsShownX(3);
            //SetColor("Temperature", Color.red);
            //SetColor("CO2", Color.blue);
            //DrawInitialValues();
        }

        public void CustomizeGraph(string config)
        {
            

            string unit = "";
            string topic = "";
            string categorie = topic + " in " + unit;
        }

        public void SetColor(string topic, Color c)
        {
            //Material fill = new Material(graphFill);
            Material fill = new Material(lineMaterial);
            fill.color = c;
            // print(c.ToString());
            graph.DataSource.SetCategoryLine(topic, fill, 5, new MaterialTiling(false, 0));
        }

        private void SetNumDataPointsShown(string categorie, int num)
        {
            //graph.DataSource.AddCategory();
            //graph.DataSource.AddCategory();
        }

        public string SetNumLabelsShownX(int num)
        {
            if (num < 0 || num > 10)
            {
                return "Invalid Amount of Labels on X Axis";
            }
            horizontalAxis.MainDivisions.Total = num;
            horizontalAxis.SubDivisions.Total = 1;
            return success;
        }

        public string SetNumLabelsShownY(int num)
        {
            if (num <= 0 || num >= 10)
            {
                return "Invalid Amount of Labels on Y Axis";
            }
            verticalAxis.MainDivisions.Total = num;
            verticalAxis.SubDivisions.Total = 1;
            return success;
        }

        public void AddDataPoint(string topic, DateTime time, float val)
        {
            graph.DataSource.AddPointToCategoryRealtime(topic, time, val, 0f);
            //WidgetFactory.Instance.curvedUI..AddEffectToChildren();
        }
    }
}
/*
private void Update()
{
            
            float time = Time.time;
            if (lastTime + 2f < time)
            {
                //gv.SetColor("Player 1", new Color(Random.value, Random.value, Random.value));

                //Graph.DataSource.Poin

                lastTime = time;
                lastX += Random.value * 3f;
    //            System.DateTime t = ChartDateUtility.ValueToDate(lastX);

                if (Random.value < 1.5)
                {
                    graph.DataSource.AddPointToCategoryRealtime("Temperature", System.DateTime.Now, Random.value * 20f + 10f,0f); // each time we call AddPointToCategory 
                    //SetColor("Temperature", new Color(Random.value, Random.value, Random.value));
                }

                //graph.DataSource.AddPointToCategoryRealtime("CO2", System.DateTime.Now, Random.value * 10f, 0f); // each time we call AddPointToCategory

            }
            
        
        }

    

        public void DrawInitialValues()
        {
            float x = 3f * TotalPoints;
            graph.DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
            graph.DataSource.ClearCategory("Temperature"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
                                                           //graph.DataSource.ClearCategory("CO2"); // clear the "Player 2" category. this category is defined using the GraphChart inspector

            for (int i = 0; i < TotalPoints; i++)  //add random points to the graph
            {
                graph.DataSource.AddPointToCategoryRealtime("Temperature", System.DateTime.Now - System.TimeSpan.FromSeconds(i), Random.value * 20f + 10f); // each time we call AddPointToCategory 
                                                                                                                                                            //graph.DataSource.AddPointToCategoryRealtime("CO2", System.DateTime.Now  - System.TimeSpan.FromSeconds(i), Random.value * 10f); // each time we call AddPointToCategory 
                x -= Random.value * 3f;
                lastX = x;
            }

            //graph.DataSource.AddPointToCategoryRealtime("Temperature", System.DateTime.Now, Random.value * 20f + 10f, 1f); // each time we call AddPointToCategory 
            //graph.DataSource.AddPointToCategoryRealtime("CO2", System.DateTime.Now, Random.value * 10f, 1f); // each time we call AddPointToCategory


            graph.DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        }
            */
