using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO!!
namespace Widgets
{
    public class TextBannerView : View
    {
        private TextFieldManager textFieldManager;

        public override void Init(Model model)
        {
            /*iconManager = GetComponent<IconStateManager>();
            graphManager = GetComponentInChildren<GraphManager>();
            graphManager.Init();
            iconManager.DetailedPanel.SetActive(false);*/

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
            if (pos - 1 < 0 || pos - 1 > GUIData.Instance.positionsText.Length)
            {
                Debug.LogWarning("Invalid position for widget:" + pos);
                return;
            }
            // TODO: Make positionText of Type TextFieldManager[]
            textFieldManager = GUIData.Instance.positionsText[pos - 1].GetComponent<TextFieldManager>();
        }
    }
}