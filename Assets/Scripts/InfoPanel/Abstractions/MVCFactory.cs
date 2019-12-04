using UnityEngine;

public class MVCFactory : MonoBehaviour
{
    const int SIZE = 10;

    Controller<DataPoint<float>> controller;

    View<DataPoint<float>> view;

    DisplayView inspectorView;

    public int counter = 0;

    private void Start()
    {
        Model<DataPoint<float>> model = new Model<DataPoint<float>>();
        model.InitializeModel(SIZE);
               
        controller = new Controller<DataPoint<float>>(model);

        inspectorView = gameObject.AddComponent<DisplayView>();
        inspectorView.Init(SIZE);

        view = new View<DataPoint<float>>(inspectorView);

        controller.AddView(view);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            controller.AddDataPoint(new DataPoint<float>(Time.time, counter++, Random.value));
        }
    }    
}
