using System.IO;
using UnityEngine;

namespace Widgets
{
    public class ToastrController : Controller
    {
        public ToastrController(ToastrModel model) : base(model)
        {
        }

        public override void ReceiveMessage(RosJsonMessage rosMessage)
        {
            ToastrModel toastrModel = (ToastrModel)model;

            if (rosMessage.toastrColor != null)
            {
                toastrModel.ChangeColor(WidgetUtility.BytesToColor(rosMessage.toastrColor));
            }

            if (rosMessage.toastrDuration != 0.0f)
            {
                toastrModel.ChangeDuration(rosMessage.toastrDuration);
            }

            if (rosMessage.toastrFontSize != 0)
            {
                toastrModel.ChangeFontSize(rosMessage.toastrFontSize);
            }

            if (!rosMessage.toastrMessage.Equals(""))
            {
                toastrModel.EnqueueNewMessage(rosMessage.toastrMessage);
            }
        }
    }
}