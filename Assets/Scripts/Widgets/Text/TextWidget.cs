using UnityEngine;

namespace Widgets
{
    public class TextWidget : Widget
    {
        TextView view;

        public Color color;
        public int fontSize;

        public string displayedMessage;

        public override View GetView()
        {
            return view;
        }

        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            throw new System.NotImplementedException();
        }

        public new void Init(RosJsonMessage rosJsonMessage)
        {            
            color = WidgetUtility.BytesToColor(rosJsonMessage.textColor);
            fontSize = rosJsonMessage.textFontSize;
            
            base.Init(rosJsonMessage);
        }

        private void changeDisplayedMessage(string messageToDisplay, Color textColor, int textFontSize)
        {
            if (messageToDisplay.Equals(""))
            {
                return;
            }

            if (textColor == null)
            {
                textColor = color;
            }
            
            if (textFontSize == 0)
            {
                textFontSize = fontSize;
            }

            displayedMessage = messageToDisplay;

            view.ChangeMessage();
        }

        public override void RestoreViews(GameObject viewParent)
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateInClass()
        {
            throw new System.NotImplementedException();
        }
    }
}