using System.Collections.Generic;

public class Controller<T>
{
    private Model<T> model;

    private List<View<T>> views;

    public Controller(Model<T> model)
    {
        this.model = model;
    }

    public void AddDataPoint(T dataPoint)
    {
        model.AddDataPoint(dataPoint);
    }

    public void AddView(View<T> viewToAdd)
    {
        if (views == null)
        {
            views = new List<View<T>>();
        }

        views.Add(viewToAdd);
        model.Subscribe(viewToAdd);
    }

    public void RemoveView(View<T> viewToRemove)
    {
        if (views == null)
            return;

        views.Remove(viewToRemove);
        model.Unsubscribe(viewToRemove);
    }
}
