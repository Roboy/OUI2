using System;
using UnityEngine;

public class Controller<T>
{
    private Model<T> model;

    private View<T> view;

    public Controller(Model<T> model)
    {
        this.model = model;
        view = new View<T>();
    }

    public void AddDataPoint(T dataPoint)
    {
        model.AddDataPoint(dataPoint);
        view.UpdateView(model.GetData());
    }
}
