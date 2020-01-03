using System.Collections.Generic;
using UnityEngine;

public class WidgetFactory : Singleton<WidgetFactory>
{ 
    // Start is called before the first frame update
    public List<Widget> CreateWidgetsAtStartup()
    {
        List<WidgetContext> widgetContexts = JsonReceiver.ParseAllWidgetTemplates();

        List<Widget> widgets = new List<Widget>();

        foreach (WidgetContext widgetContext in widgetContexts)
        {
            Widget createdWidget = CreateWidgetFromContext(widgetContext, widgets);

            if (createdWidget == null)
            {
                Debug.Log("null");
                continue;
            }

            widgets.Add(createdWidget);
        }

        return widgets;
    }

    private bool IsWidgetIdUnique(WidgetContext widgetContext, List<Widget> existingWidgets)
    {
        foreach (Widget existingWidget in existingWidgets)
        {
            if (existingWidget.GetID() == widgetContext.template_ID)
            {
                Debug.LogWarning("duplicate ID: " + widgetContext.template_ID + " in widget templates");
                return false;
            }
        }

        return true;
    }

    public Widget CreateWidgetFromContext(WidgetContext widgetContext, List<Widget> existingWidgets)
    {
        if (IsWidgetIdUnique(widgetContext, existingWidgets) == false)
        {
            return null;
        }

        Widget widget = null; 
        
        switch (widgetContext.type)
        {
            case LiveDataType.Graph:
                widget = CreateGraphWidget(widgetContext);
                break;

            case LiveDataType.Inspector:
                break;

            default:
                Debug.LogWarning("Type not defined: " + widgetContext.type);
                break;
        }        

        return widget;

        /*
        AbstractController controller;
        AbstractModel model;
        AbstractView view;

        if (widgetContext.type == graph)
        {
            view = new GraphView(widgetContext.color);
        }

        Widget widget = new Widget(controller, model, view);

        return widget;
        */
    }

    public Widget CreateGraphWidget(WidgetContext widgetContext)
    {
        View view = WidgetManager.Instance.gameObject.AddComponent<GraphViewDummy>() as GraphViewDummy;
        Model model = new GraphModel(view, widgetContext.pos, widgetContext.color);
        Controller controller = new GraphController(model);
        Widget widget = WidgetManager.Instance.gameObject.AddComponent<Widget>() as Widget;

        widget.InitializeWidget(controller, model, view);

        return widget;
    }
}
