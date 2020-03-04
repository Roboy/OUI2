using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{
    public class GraphView : View
    {
        private IconStateManager iconManager;
        private GraphManager graphManager;

        private GameObject CreateGraphlikeObject()
        {
            GameObject graph = new GameObject();
            graph.transform.SetParent(transform);
            graph.AddComponent<RectTransform>();
            graph.transform.localScale = Vector3.one;

            graph.AddComponent<CurvedUI.CurvedUIVertexEffect>();
            return graph;
        }

        private GameObject CreateGraph()
        {
            GameObject graph = CreateGraphlikeObject();
            graph.transform.localPosition = Vector3.zero;
            ((RectTransform)(graph.transform)).sizeDelta = new Vector2(600, 300);
            graph.AddComponent<ChartAndGraph.GraphChart>();
            graph.AddComponent<ChartAndGraph.VerticalAxis>();
            graph.AddComponent<ChartAndGraph.HorizontalAxis>(); // todo: should be hand made
            graph.AddComponent<ChartAndGraph.SensitivityControl>();
            graph.AddComponent<GraphManager>();
            return graph;
        }

        private GameObject CreateGraphAdditions(GraphWidget graphWidget)
        {
            GameObject additions = CreateGraphlikeObject();
            additions.transform.localPosition = Vector3.forward;
            RectTransform rectTrans = ((RectTransform)(additions.transform));
            //rectTrans.

            TextMeshProUGUI title = additions.AddComponent<TextMeshProUGUI>();
            title.text = graphWidget.name;
            if (graphWidget.unit == null)
            {
                title.text += " in " + graphWidget.unit;
            }
            title.text += " over the last 10s";

            title.fontSize = 30;
            
            rectTrans.sizeDelta = new Vector2(650, 380);
            additions.AddComponent<CurvedUI.CurvedUIVertexEffect>();
            return additions;
        }

        //public void Init(List<float> dataPoints, Widget childWidget, string graphName, Color color)
        public override void Init(Widget widget)
        { 
            gameObject.AddComponent<CurvedUI.CurvedUIVertexEffect>();
            GraphWidget graphWidget = (GraphWidget)widget;
            SetChildWidget(graphWidget.childWidget);
        
            GameObject graph = CreateGraph();
            GameObject graphAdditions = CreateGraphAdditions(graphWidget);
            graphManager = graph.GetComponent<GraphManager>();

            graphManager.Init(graphWidget);
            graphManager.SetColor(graphWidget.name, graphWidget.color);
            graphManager.SetNumLabelsShownX(graphWidget.numXLabels);
            graphManager.SetNumLabelsShownY(graphWidget.numYLabels);
            foreach (GraphWidget.Datapoint data in graphWidget.datapoints)
            {
                graphManager.AddDataPoint(graphWidget.name, data.time,
                    data.data);
            }

            Init(widget.relativeChildPosition);

            /*
            //iconManager = GetComponent<IconStateManager>();
            graphManager = GetComponentInChildren<GraphManager>();
            graphManager.Init();
            iconManager.DetailedPanel.SetActive(false);
            //SetPosition(panel_id); // test
            //SetPosition(((GraphModel)model).GetPanelId());
            UpdateView(widget);*/
        }

        // not in use atm and outdated
        public void UpdateView(Widget widget)
        {
            GraphWidget graphWidget = (GraphWidget)widget;
            //SetDetailedPanelPosition(1); // graphModel.detailedPanelPos
            graphManager.SetColor(graphWidget.name, graphWidget.color);
            graphManager.SetNumLabelsShownX(graphWidget.numXLabels);
            graphManager.SetNumLabelsShownY(graphWidget.numYLabels);
            if (graphWidget.datapoints.Count > 0)
            {
                GraphWidget.Datapoint dp = graphWidget.datapoints[graphWidget.datapoints.Count - 1];
                graphManager.AddDataPoint(graphWidget.name, dp.time, dp.data);
                    //graphWidget.datapoints.ToArray()[graphWidget.datapoints.Count - 1]);
            }
            // Debug.Log("View updated");

        }

        private void SetPosition(int pos)
        {
            if (pos - 1 < 0 || pos - 1 > GUIData.Instance.positionsUI.Length)
            {
                Debug.LogWarning("Invalid position for widget:" + pos);
                return;
            }
            transform.SetParent(GUIData.Instance.positionsUI[pos - 1].transform);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }

        
        private void SetDetailedPanelPosition(int pos)
        {
            if (pos - 1 < 0 || pos - 1 > GUIData.Instance.positionsDetailedPanels.Length)
            {
                Debug.LogWarning("Invalid position of Detailed Panel for widget:" + pos);
                return;
            }

            iconManager.DetailedPanel.transform.SetParent(GUIData.Instance.positionsDetailedPanels[pos - 1].transform);
            RectTransform rect = iconManager.DetailedPanel.GetComponent<RectTransform>();
            rect.offsetMin = Vector3.zero;
            rect.offsetMax = Vector3.zero;
            //iconManager.DetailedPanel.transform.SetParent(transform);
            // TODO: set the panel to the correct x position
            // iconManager.DetailedPanel.transform.localPosition -= Vector3.right * transform.position.x; //120; // iconManager.DetailedPanel.transform.position.x
            //rect.offsetMin = Vector3.zero;
            //rect.offsetMax = Vector3.zero;

            //iconManager.DetailedPanel.transform.localScale = Vector3.one;
        }
        

        private void SetThumbnailIcon(string iconName)
        {
            // TODO
        }

        /*private void SetColor(string title, Color col)
        {
            // TODO: parse Color
            graphManager.SetColor(title, col);
        }*/

        public override void ShowView(RelativeChildPosition relativeChildPosition)
        {
            gameObject.SetActive(true);
        }

        public override void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}
