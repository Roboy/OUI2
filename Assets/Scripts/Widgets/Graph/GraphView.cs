using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GraphView : MonoBehaviour
    {
        private IconStateManager iconManager;
        private GraphManager graphManager;

        public void Init(Widget widget, int panel_id)
        {
            iconManager = GetComponent<IconStateManager>();
            graphManager = GetComponentInChildren<GraphManager>();
            graphManager.Init();
            iconManager.DetailedPanel.SetActive(false);
            SetPosition(panel_id); // test
            //SetPosition(((GraphModel)model).GetPanelId());
            UpdateView(widget);
        }

        public void UpdateView(Widget widget)
        {
            GraphWidget graphWidget = (GraphWidget)widget;
            SetDetailedPanelPosition(1); // graphModel.detailedPanelPos
            SetColor(graphWidget.name, graphWidget.color);
            if (graphWidget.datapoints.Count > 0)
            {
                graphManager.AddDataPoint(graphWidget.name, System.DateTime.Now,
                    graphWidget.datapoints.ToArray()[graphWidget.datapoints.Count - 1]);
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
