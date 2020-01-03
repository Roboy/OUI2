using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : View
{
    private IconStateManager iconManager;
    private GraphManager graphManager;

    public override void Init(Model model)
    {
        iconManager = GetComponent<IconStateManager>();
        graphManager = GetComponentInChildren<GraphManager>();
        graphManager.Init();
        iconManager.DetailedPanel.SetActive(false);
        UpdateView(model);
    }

    public override void UpdateView(Model model)
    {
        GraphModel graphModel = (GraphModel)model;
        SetPosition(graphModel.GetPos());
        SetDetailedPanelPosition(graphModel.detailedPanelPos);
        SetColor(model.title, graphModel.color);
        Debug.Log("View updated");
    }

    private void SetPosition(int pos)
    {
        if (pos - 1 < 0 || pos - 1 > WidgetPositions.Instance.positionsUI.Length)
        {
            Debug.LogWarning("Invalid position for widget:" + pos);
            return;
        }
        transform.SetParent(WidgetPositions.Instance.positionsUI[pos - 1].transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    private void SetDetailedPanelPosition(int pos)
    {
        if (pos - 1 < 0 || pos - 1 > WidgetPositions.Instance.positionsDetailedPanels.Length)
        {
            Debug.LogWarning("Invalid position of Detailed Panel for widget:" + pos);
            return;
        }
        iconManager.DetailedPanel.transform.SetParent(WidgetPositions.Instance.positionsDetailedPanels[pos - 1].transform);
        iconManager.DetailedPanel.GetComponent<RectTransform>().offsetMin = Vector3.zero;
        iconManager.DetailedPanel.GetComponent<RectTransform>().offsetMax = Vector3.zero;
        //iconManager.DetailedPanel.transform.localScale = Vector3.one;
    }

    private void SetThumbnailIcon(string iconName)
    {
        // TODO
    }

    private void SetColor(string title, int col)
    {
        // TODO: parse Color
        graphManager.SetColor(title, new Color(1, 1, 0));
    }
}
