using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphViewDummy : View
{
    private IconStateManager iconManager;

    public override void Init()
    {
        iconManager = GetComponent<IconStateManager>();
    }

    public override void UpdateView(Model model)
    {
        GraphModel graphModel = (GraphModel)model;
        SetPosition(graphModel.GetPos());
        SetDetailedPanelPosition(graphModel.detailedPanelPos);
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
}
