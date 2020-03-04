using TMPro;
using UnityEngine;
using static Widgets.TextWidget;

namespace Widgets
{
    public class TextView : View
    {
        TextMeshProUGUI textMeshPro;

        public void Init(TextWidgetTemplate textWidgetTemplate, Widget childWidget)
        {
            this.childWidget = childWidget;
            ChangeMessage(textWidgetTemplate);
        }

        public override void HideView()
        {
            throw new System.NotImplementedException();
        }

        public override void ShowView()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeMessage(TextWidgetTemplate incomingMessageTemplate)
        {
            textMeshPro.text = incomingMessageTemplate.messageToDisplay;
            textMeshPro.color = incomingMessageTemplate.textColor;
            textMeshPro.fontSize = incomingMessageTemplate.textFontSize;
        }
    }
}