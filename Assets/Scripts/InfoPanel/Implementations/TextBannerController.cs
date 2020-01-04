using System.IO;

public class TextBannerController : Controller
{
    public TextBannerController(Model model) : base(model)
    {
    }

    public override void ReceiveMessage(byte[] rosMessageData)
    {
        model.UpdateModel(ParseMessage(rosMessageData));
    }

    private TextBannerMessage ParseMessage(byte[] rosMessageData)
    {
        Stream dataStream = new MemoryStream(rosMessageData);
        BinaryReader binaryReader = new BinaryReader(dataStream);

        return new TextBannerMessage(binaryReader.ReadInt32(), binaryReader.ReadSingle(), binaryReader.ReadInt32(), binaryReader.ReadInt32(), binaryReader.ReadString());

        //return new TextBannerMessage("Test Message", 1, 5, 1, 16);
    }
}

public class TextBannerMessage : WidgetMessage
{
    public float duration;
    public int color;
    public int fontSize;
    public string msg;

    public TextBannerMessage(int pos, float duration, int color, int fontSize, string msg) : base(pos)
    {
        this.duration = duration;
        this.color = color;
        this.fontSize = fontSize;
        this.msg = msg;
    }
}