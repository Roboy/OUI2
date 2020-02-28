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

            toastrModel.EnqueueNewMessage(rosMessage.toastrMessage, rosMessage.toastrDuration, WidgetUtility.BytesToColor(rosMessage.toastrColor), rosMessage.toastrFontSize);
        }

        public override void Update()
        {
            ((ToastrModel)model).Update();
        }
    }
}