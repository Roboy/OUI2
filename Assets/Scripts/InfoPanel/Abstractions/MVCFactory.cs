using UnityEngine;

public class MVCFactory : MonoBehaviour
{
    const int SIZE = 10;

    Controller<DataPoint<float>> controller;

    View<DataPoint<float>> view;

    InspectorDisplay inspectorDisplay;

    public int counter = 0;

    private void Start()
    {
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++  INIT THE MODEL  ++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        Model<DataPoint<float>> model = new Model<DataPoint<float>>(SIZE);

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++  ADD A CONTROLLER TO THE MODEL  +++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        controller = CreateControllerFloat(model);

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++  ADD A VIEW WITH A DISPLAY TO THE MODEL  ++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        view = CreateInspectorViewFloat(model);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            controller.AddDataPoint(new DataPoint<float>(Time.time, counter++, Random.value));
        }
    }    

    public Controller<DataPoint<float>> CreateControllerFloat(Model<DataPoint<float>> model)
    {
        return new Controller<DataPoint<float>>(model);
    }

    public View<DataPoint<float>> CreateInspectorViewFloat(Model<DataPoint<float>> model)
    {
        inspectorDisplay = gameObject.AddComponent<InspectorDisplay>();
        inspectorDisplay.Init(SIZE);
        return new InspectorView<DataPoint<float>, float>(inspectorDisplay, model);
    }
}
