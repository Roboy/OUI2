namespace Widgets
{
    public class InspectorGraphView : View
    {
        public string[] displayArray;

        public override void Init(Model model)
        {
            displayArray = new string[GraphModel.SIZE];
        }

        public override void UpdateView(Model model)
        {
            GraphModel graphModel = (GraphModel)model;

            float[] dataArray = graphModel.datapoints.ToArray();

            for (int i = 0; i < dataArray.Length; i++)
            {
                displayArray[i] = dataArray[i].ToString();
            }
        }
    }
}