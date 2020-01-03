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
        return new TextBannerMessage(1.0f, 1, 1, 1);
    }
}

public class TextBannerMessage : WidgetMessage
{
    public float datapoint;
    public int detailedPanelPos;
    public int color;

    public TextBannerMessage(float datapoint, int pos, int detailedPanelPos, int color) : base(pos)
    {
        this.datapoint = datapoint;
        this.detailedPanelPos = detailedPanelPos;
        this.color = color;
    }
}