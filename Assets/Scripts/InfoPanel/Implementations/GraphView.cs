using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : View
{
    private IconStateManager iconManager;
    private GraphManager graphManager;

    public override void Init(Model model)
    {
        GraphModel graphModel = (GraphModel)model;

        iconManager = GetComponent<IconStateManager>();
        graphManager = GetComponentInChildren<GraphManager>();
        graphManager.Init();
        iconManager.DetailedPanel.SetActive(false);
        SetPosition((graphModel).GetPos());
        SetThumbnailIcon(graphModel.iconName);
        SetThumbnailText(graphModel.thumbnailText);
        UpdateView(model);
    }

    public override void UpdateView(Model model)
    {
        GraphModel graphModel = (GraphModel)model;
        SetDetailedPanelPosition(graphModel.detailedPanelPos);
        SetColor(graphModel.title, graphModel.color);
        if (graphModel.datapoints.Count > 0)
        { 
            graphManager.AddDataPoint(graphModel.title, System.DateTime.Now, graphModel.datapoints.ToArray()[graphModel.datapoints.Count-1]);
        }
        // Debug.Log("View updated");
        
    }

    private void SetPosition(int pos)
    {
        if (pos - 1 < 0 || pos - 1 > WidgetData.Instance.positionsUI.Length)
        {
            Debug.LogWarning("Invalid position for widget:" + pos);
            return;
        }
        transform.SetParent(WidgetData.Instance.positionsUI[pos - 1].transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    private void SetDetailedPanelPosition(int pos)
    {
        if (pos - 1 < 0 || pos - 1 > WidgetData.Instance.positionsDetailedPanels.Length)
        {
            Debug.LogWarning("Invalid position of Detailed Panel for widget:" + pos);
            return;
        }

        iconManager.DetailedPanel.transform.SetParent(WidgetData.Instance.positionsDetailedPanels[pos - 1].transform);
        RectTransform rect = iconManager.DetailedPanel.GetComponent<RectTransform>();
        rect.offsetMin = Vector3.zero;
        rect.offsetMax = Vector3.zero;
        //iconManager.DetailedPanel.transform.SetParent(transform, true);
        // TODO: set the panel to the correct x position
        ////iconManager.DetailedPanel.transform.localPosition = iconManager.DetailedPanel.transform.localPosition -= Vector3.right * 120; // iconManager.DetailedPanel.transform.position.x
        //rect.offsetMin = Vector3.zero;
        //rect.offsetMax = Vector3.zero;

        //iconManager.DetailedPanel.transform.localScale = Vector3.one;
    }

    private void SetThumbnailIcon(string iconName)
    {
        print("icon: " + "Icons/" + iconName);
        iconManager.thumbnailIcon.gameObject.SetActive(iconName != null);
        if (iconName == null) return;
        iconManager.thumbnailIcon.sprite = (Sprite)Resources.Load("Icons/" + iconName, typeof(Sprite));
        print(iconManager.thumbnailIcon.sprite);
    }

    private void SetThumbnailText(string text)
    {
        print("text: " + text);
        iconManager.thumbnailText.gameObject.SetActive(text != null);
        iconManager.thumbnailText.text = text;
    }

    private void SetColor(string title, Color col)
    {
        // TODO: parse Color
        graphManager.SetColor(title, col);
    }
}
