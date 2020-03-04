using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Widgets.TextWidget;

namespace Widgets
{
    public class TextView : View
    {
        TextMeshProUGUI textMeshPro;
        RawImage image;

        private void Awake()
        {
            textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();
        }
        
        public override void HideView()
        {
            textMeshPro.enabled = false;
        }

        public override void ShowView()
        {
            textMeshPro.enabled = true;
        }

        public void ChangeMessage(TextWidgetTemplate incomingMessageTemplate)
        {
            textMeshPro.text = incomingMessageTemplate.messageToDisplay;
            textMeshPro.color = incomingMessageTemplate.textColor;
            textMeshPro.fontSize = incomingMessageTemplate.textFontSize;
        }

        public override void Init(Widget widget)
        {
            SetChildWidget(widget.childWidget);
            AttachCurvedUI();
            ChangeMessage(((TextWidget)widget).currentlyDisplayedMessage);
        }
    }
}