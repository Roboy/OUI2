using System.IO;
using UnityEngine;

public class GraphController : Controller
{
    public GraphController(Model model) : base(model)
    {
    }

    public override void ReceiveMessage(RosJsonMessage rosMessage)
    {
        model.UpdateModel(ParseMessage(rosMessage));
    }

    private GraphMessage ParseMessage(RosJsonMessage rosMessage)
    {
        return new GraphMessage(rosMessage.datapoint, 0, 0, Color.red);
    }
}

public class GraphMessage : WidgetMessage
{
    public float datapoint;
    public int detailedPanelPos;
    public Color color;

    public GraphMessage(float datapoint, int pos, int detailedPanelPos, Color color) : base(pos)
    {
        this.datapoint = datapoint;
        this.detailedPanelPos = detailedPanelPos;
        this.color = color;
    }
}