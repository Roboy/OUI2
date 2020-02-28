using System.IO;
using UnityEngine;

namespace Widgets
{
    public class GraphController : Controller
    {
        public GraphController(GraphModel model) : base(model)
        {
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

        public override void Update()
        {
           
        }
    }
}