using System.Collections.Generic;
using UnityEngine;

public abstract class Model
{
    protected View view;

    protected int pos;

    public string title;

    public Model (View view, int pos, string title)
    {
        this.view = view;
        this.pos = pos;
        this.title = title;
    }

    // TODO: public or getters?
    public int GetPos()
    {
        return pos;
    }
    
    public abstract void UpdateModel(WidgetMessage newMessage);
}
