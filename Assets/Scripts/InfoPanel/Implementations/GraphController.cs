public class GraphController : Controller
{
    public GraphController(Model model) : base(model)
    {
    }

    public override void ReceiveMessage(byte[] message)
    {
        model.UpdateModel(ParseMessage());
    }

    private GraphMessage ParseMessage()
    {
        return new GraphMessage(1.0f, 2, 1);
    }
}

public class GraphMessage : WidgetMessage
{
    public float datapoint;
    public int color;

    public GraphMessage(float datapoint, int pos, int color) : base(pos)
    {
        this.datapoint = datapoint;
        this.color = color;
    }
}