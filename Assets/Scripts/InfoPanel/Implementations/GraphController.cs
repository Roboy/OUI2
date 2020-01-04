using System.IO;
using UnityEngine;

public class GraphController : Controller
{
    public GraphController(Model model) : base(model)
    {
    }

    public override void ReceiveMessage(byte[] rosMessageData)
    {
        model.UpdateModel(ParseMessage(rosMessageData));
    }

    private GraphMessage ParseMessage(byte[] rosMessageData)
    {
        Stream dataStream = new MemoryStream(rosMessageData);
        BinaryReader binaryReader = new BinaryReader(dataStream);

        Debug.Log("New message arrived");

        return new GraphMessage(binaryReader.ReadSingle(), binaryReader.ReadInt32(), binaryReader.ReadInt32(), binaryReader.ReadInt32());
    }
}

public class GraphMessage : WidgetMessage
{
    public float datapoint;
    public int detailedPanelPos;
    public int color;

    public GraphMessage(float datapoint, int pos, int detailedPanelPos, int color) : base(pos)
    {
        this.datapoint = datapoint;
        this.detailedPanelPos = detailedPanelPos;
        this.color = color;
    }
}