public class InspectorView : View
{
    public string[] display;

    public override void Init(Model model)
    {
        display = new string[GraphModel.SIZE];
    }

    public override void UpdateView(Model model)
    {
        GraphModel graphModel = (GraphModel)model;

        float[] dataArray = graphModel.datapoints.ToArray();

        for (int i = 0; i < dataArray.Length; i++)
        {
            display[i] = dataArray[i].ToString();
        }
    }
}

