using System.IO;
using UnityEngine;

namespace Widgets
{
    public class GraphController : Controller
    {
        public void Init(GraphModel model)
        {
            base.Init(model);
        }

        public override void ReceiveMessage(RosJsonMessage rosMessage)
        {
            GraphModel graphModel = (GraphModel)model;

            if (rosMessage.graphColor != null)
            {
                graphModel.ChangeColor(WidgetUtility.BytesToColor(rosMessage.graphColor));
            }

            if (rosMessage.graphDatapoint != 0.0f)
            {
                graphModel.AddDatapoint(rosMessage.graphDatapoint);
            }
        }
    }
}