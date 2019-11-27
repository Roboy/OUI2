using UnityEngine;

public class MVCFactory : MonoBehaviour
{
    Controller<int> controller;

    public int counter = 0;

    private void Start()
    {
        Model<int> model = new Model<int>();
        model.InitializeModel(10);
               
        controller = new Controller<int>(model);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            controller.AddDataPoint(counter++);
    }    
}
