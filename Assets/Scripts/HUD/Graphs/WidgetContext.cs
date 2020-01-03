using UnityEngine;

public class WidgetContext
{
    public int template_ID;
    public LiveDataType type;
    public byte[] data;
    public int pos;
    
    // determines how many labels should be shown on the x axis of the graph
    public int numLabelsShownX;
    // determines how many labels should be shown on the y axis of the graph
    public int numLabelsShownY;
    // the color of the graph
    public int color;
    
    public WidgetContext(int template_ID, string type, byte[] data, int pos, int numLabelsShownX, int numLabelsShownY, int color)
    {
        this.template_ID = template_ID;

        switch(type)
        {
            case "graph":
                this.type = LiveDataType.Graph;
                break;

            case "inspector":
                this.type = LiveDataType.Inspector;
                break;

            default:
                Debug.LogWarning("Type of widget template " + template_ID + " not correct.");
                break;
        }

        this.data = data;
        this.pos = pos;
        this.numLabelsShownX = numLabelsShownX;
        this.numLabelsShownY = numLabelsShownY;
        this.color = color;
    }
}

public enum LiveDataType
{
    Graph, Inspector
}
