using System.IO;
using UnityEngine;

public class TextBannerController : Controller
{
    public TextBannerController(Model model) : base(model)
    {
    }

    public override void ReceiveMessage(JSON_message rosMessage)
    {
        model.UpdateModel(ParseMessage(rosMessage));
    }

    private TextBannerMessage ParseMessage(JSON_message rosMessage)
    {        
        /*
        Stream dataStream = new MemoryStream(rosMessageData);
        BinaryReader binaryReader = new BinaryReader(dataStream);

        return new TextBannerMessage(binaryReader.ReadInt32(), binaryReader.ReadSingle(), WidgetUtility.IntToColor(binaryReader.ReadInt32()), binaryReader.ReadInt32(), binaryReader.ReadString());

        //return new TextBannerMessage("Test Message", 1, 5, 1, 16);  
        */

        return new TextBannerMessage(0, 1, Color.blue, 12, rosMessage.datapoint.ToString());
    }
}

public class TextBannerMessage : WidgetMessage
{
    public float duration;
    public Color color;
    public int fontSize;
    public string msg;

    public TextBannerMessage(int pos, float duration, Color color, int fontSize, string msg) : base(pos)
    {
        this.duration = duration;
        this.color = color;
        this.fontSize = fontSize;
        this.msg = msg;
    }
}