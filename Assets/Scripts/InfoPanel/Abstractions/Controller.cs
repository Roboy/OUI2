
public abstract class Controller
{
    protected Model model;

    public Controller(Model model)
    {
        this.model = model;
    }

    public abstract void ReceiveMessage(JSON_message rosMessage);
}

public abstract class WidgetMessage
{   
    public int pos;

    public WidgetMessage(int pos)
    {
        this.pos = pos;
    }
}
