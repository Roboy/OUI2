using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Widgets.TextWidget;

namespace Widgets
{
    public class TextView : View
    {
        private readonly float RELATIVE_OFFSET = 100.0f;

        TextMeshProUGUI textMeshPro;
        RawImage[] images;
                
        public override void HideView()
        {
            textMeshPro.enabled = false;

            foreach (RawImage image in images)
            {
                image.enabled = false;
            }
        }

        public override void ShowView(RelativeChildPosition relativeChildPosition)
        {
            textMeshPro.enabled = true;

            foreach (RawImage image in images)
            {
                image.enabled = true;
            }


            Vector3 offsetVector = Vector3.zero;

            switch (relativeChildPosition)
            {
                case RelativeChildPosition.Bottom:
                    offsetVector = Vector3.down * RELATIVE_OFFSET;
                    break;
                case RelativeChildPosition.Top:
                    offsetVector = Vector3.up * RELATIVE_OFFSET;
                    break;
                case RelativeChildPosition.Left:
                    offsetVector = Vector3.left * RELATIVE_OFFSET;
                    break;
                case RelativeChildPosition.Right:
                    offsetVector = Vector3.right * RELATIVE_OFFSET;
                    break;
                case RelativeChildPosition.FixedCenter:
                    return;
                case RelativeChildPosition.Incorrect:                    
                    return;
            }

            gameObject.AddComponent<CurvedUI.CurvedUIVertexEffect>();

            transform.SetParent(parentView.transform, false);

            transform.localPosition = offsetVector;
        }

        public void ChangeMessage(TextWidgetTemplate incomingMessageTemplate)
        {
            textMeshPro.text = incomingMessageTemplate.messageToDisplay;
            textMeshPro.color = incomingMessageTemplate.textColor;
            textMeshPro.fontSize = incomingMessageTemplate.textFontSize;
        }

        public override void Init(Widget widget)
        {
            GameObject viewDesign = Instantiate(widget.viewDesignPrefab);
            viewDesign.transform.SetParent(this.transform, false);
            images = gameObject.GetComponentsInChildren<RawImage>();
            textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();

            SetChildWidget(widget.childWidget);
            ChangeMessage(((TextWidget)widget).currentlyDisplayedMessage);

            Init(widget.relativeChildPosition);
        }
    }
}