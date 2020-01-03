public class TextBannerController : Controller
{
    public TextBannerController(Model model) : base(model)
    {
    }

    public override void ReceiveMessage(byte[] message)
    {
        model.UpdateModel(ParseMessage());
    }

    private TextBannerMessage ParseMessage()
    {
        return new TextBannerMessage("Test Message", 1, 5, 1, 16);
    }
}

public class TextBannerMessage : WidgetMessage
{
    public string msg;
    public float duration;
    public int color;
    public int fontSize;

    public TextBannerMessage(string msg, int pos, float duration, int color, int fontSize) : base(pos)
    {
        this.msg = msg;
        this.duration = duration;
        this.color = color;
        this.fontSize = fontSize;
    }
}