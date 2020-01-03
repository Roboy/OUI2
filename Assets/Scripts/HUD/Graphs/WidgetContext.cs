using UnityEngine;

public class WidgetContext
{
    public int template_ID;
    public LiveDataType type;
    public string title;
    public int pos;

    // determines where the DetailedPanel should be shown
    public int detailedPanelPos;
    // determines how many labels should be shown on the x axis of the graph
    public int numLabelsShownX;
    // determines how many labels should be shown on the y axis of the graph
    public int numLabelsShownY;
    // the color of the graph
    public int color;
    
    public WidgetContext(int template_ID, string type, string title, int pos, int detailedPanelPos, int numLabelsShownX, int numLabelsShownY, int color)
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

        this.title = title;
        this.pos = pos;
        this.detailedPanelPos = detailedPanelPos;
        this.numLabelsShownX = numLabelsShownX;
        this.numLabelsShownY = numLabelsShownY;
        this.color = color;
    }
}

public enum LiveDataType
{
    Graph, Inspector
}
