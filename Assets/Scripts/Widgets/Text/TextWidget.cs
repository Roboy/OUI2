using UnityEngine;

namespace Widgets
{
    public class TextWidget : Widget
    {
        public Color color;
        public int fontSize;

        public TextWidgetTemplate currentlyDisplayedMessage;
        
        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            TextWidgetTemplate incomingMessageTemplate = new TextWidgetTemplate(rosMessage.textMessage, WidgetUtility.BytesToColor(rosMessage.textColor), rosMessage.textFontSize);
            changeDisplayedMessage(incomingMessageTemplate);
        }

        public new void Init(RosJsonMessage context)
        {            
            color = WidgetUtility.BytesToColor(context.textColor);
            fontSize = context.textFontSize;

            TextWidgetTemplate incomingMessageTemplate = new TextWidgetTemplate(context.textMessage, WidgetUtility.BytesToColor(context.textColor), context.textFontSize);
            currentlyDisplayedMessage = incomingMessageTemplate;

            base.Init(context);
        }

        private void changeDisplayedMessage(TextWidgetTemplate incomingMessageTemplate)
        {
            if (incomingMessageTemplate.messageToDisplay.Equals(""))
            {
                return;
            }

            if (incomingMessageTemplate.textColor == null)
            {
                incomingMessageTemplate.textColor = color;
            }
            
            if (incomingMessageTemplate.textFontSize == 0)
            {
                incomingMessageTemplate.textFontSize = fontSize;
            }

            currentlyDisplayedMessage = incomingMessageTemplate;

            ((TextView)view).ChangeMessage(incomingMessageTemplate);
        }

        protected override void UpdateInClass()
        {

        }

        public override View AddViewComponent(GameObject viewGameObject)
        {
            return viewGameObject.AddComponent<TextView>();
        }

        public struct TextWidgetTemplate
        {
            public string messageToDisplay;
            public Color textColor;
            public int textFontSize;

            public TextWidgetTemplate(string messageToDisplay, Color textColor, int textFontSize)
            {
                this.messageToDisplay = messageToDisplay;
                this.textColor = textColor;
                this.textFontSize = textFontSize;
            }
        }
    }
}