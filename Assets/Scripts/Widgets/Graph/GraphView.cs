﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphView : View
    {
        private IconStateManager iconManager;
        private GraphManager graphManager;

        public override void Init(Model model, int panel_id)
        {
            iconManager = GetComponent<IconStateManager>();
            graphManager = GetComponentInChildren<GraphManager>();
            graphManager.Init();
            iconManager.DetailedPanel.SetActive(false);
            SetPosition(panel_id); // test
            //SetPosition(((GraphModel)model).GetPanelId());
            UpdateView(model);
        }

        public override void UpdateView(Model model)
        {
            GraphModel graphModel = (GraphModel)model;
            SetDetailedPanelPosition(1); // graphModel.detailedPanelPos
            SetColor(graphModel.title, graphModel.color);
            if (graphModel.datapoints.Count > 0)
            {
                graphManager.AddDataPoint(graphModel.title, System.DateTime.Now, graphModel.datapoints.ToArray()[graphModel.datapoints.Count - 1]);
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

        private void SetColor(string title, Color col)
        {
            // TODO: parse Color
            graphManager.SetColor(title, col);
        }
    }
}
