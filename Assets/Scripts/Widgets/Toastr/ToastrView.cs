using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO!!
namespace Widgets
{
    public class ToastrView : View
    {
        private TextFieldManager textFieldManager;

        public override void Init(Model model, int panel_id)
        {
            this.model = model;

            /*iconManager = GetComponent<IconStateManager>();
            graphManager = GetComponentInChildren<GraphManager>();
            graphManager.Init();
            iconManager.DetailedPanel.SetActive(false);*/

        }

        public void Update()
        {

        }

        public override void UpdateView(Model model)
        {
            ToastrModel textBannerModel = (ToastrModel)model;
            //SetPosition(textBannerModel.GetPanelId());
            //SetDetailedPanelPosition(textBannerModel.detailedPanelPos);
            //SetColor(model.title, textBannerModel.color);
            textFieldManager.queue.Enqueue(textBannerModel.toastrQueue.Dequeue());
            Debug.Log("TextBannerView updated");
        }

        public void NewToastr()
        {

        }

        public void DeleteTopToastr()
        {

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