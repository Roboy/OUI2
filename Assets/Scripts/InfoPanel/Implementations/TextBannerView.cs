using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO!!
public class TextBannerView : View
{
    private TextFieldManager textFieldManager;

    public override void Init(Model model)
    {
        /*iconManager = GetComponent<IconStateManager>();
        graphManager = GetComponentInChildren<GraphManager>();
        graphManager.Init();
        iconManager.DetailedPanel.SetActive(false);*/

        // Just for testing
        ((TextBannerModel)model).datapoints.Enqueue(new Printable("testttt", 5, Color.blue, (byte)50));
        UpdateView(model);
    }

    public override void UpdateView(Model model)
    {
        TextBannerModel textBannerModel = (TextBannerModel)model;
        SetPosition(textBannerModel.GetPos());
        //SetDetailedPanelPosition(textBannerModel.detailedPanelPos);
        //SetColor(model.title, textBannerModel.color);
        textFieldManager.queue.Enqueue(textBannerModel.datapoints.Dequeue());
        Debug.Log("TextBannerView updated");
    }

    private void SetPosition(int pos)
    {
        if (pos - 1 < 0 || pos - 1 > WidgetPositions.Instance.positionsText.Length)
        {
            Debug.LogWarning("Invalid position for widget:" + pos);
            return;
        }
        // TODO: Make positionText of Type TextFieldManager[]
        textFieldManager = WidgetPositions.Instance.positionsText[pos - 1].GetComponent<TextFieldManager>();
    }

    private void SetColor(string title, int col)
    {
        // TODO: parse Color
        //graphManager.SetColor(title, new Color(1, 1, 0));
    }
}
