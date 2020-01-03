using System.Collections.Generic;
using UnityEngine;

public abstract class Model
{
    protected View view;

    protected int pos;

    public Model (View view, int pos)
    {
        this.view = view;
        this.pos = pos;
    }
    
    public abstract void UpdateModel(WidgetMessage newMessage);
}
