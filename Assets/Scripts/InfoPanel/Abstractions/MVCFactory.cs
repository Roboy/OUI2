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

        Model<DataPoint<float>> model = new Model<DataPoint<float>>();
        model.Init(SIZE);

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++  ADD A CONTROLLER TO THE MODEL  +++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        controller = new Controller<DataPoint<float>>(model);

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++  ADD A VIEW WITH A DISPLAY TO THE MODEL  ++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        inspectorDisplay = gameObject.AddComponent<InspectorDisplay>();
        inspectorDisplay.Init(SIZE);
        view = new InspectorView<DataPoint<float>, float>(inspectorDisplay, model);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            controller.AddDataPoint(new DataPoint<float>(Time.time, counter++, Random.value));
        }
    }    
}
