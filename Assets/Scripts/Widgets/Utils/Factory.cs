using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class Factory : Singleton<Factory>
    {
        public GameObject GraphPref;
        public CurvedUI.CurvedUISettings curvedUI;

        // Start is called before the first frame update
        public List<Widget> CreateWidgetsAtStartup()
        {
            List<RosJsonMessage> widgetContexts = TemplateParser.ParseAllWidgetTemplates();

            List<Widget> widgets = new List<Widget>();

            foreach (RosJsonMessage widgetContext in widgetContexts)
            {
                Widget createdWidget = CreateWidgetFromContext(widgetContext, widgets);

                if (createdWidget == null)
                {
                    Debug.Log("widget is null");
                    continue;
                }

                widgets.Add(createdWidget);
            }

            //curvedUI.AddEffectToChildren();

            return widgets;
        }

        // TODO: What is the performance of this call?
        private void LateUpdate()
        {
            curvedUI.AddEffectToChildren();
        }

        private bool IsWidgetIdUnique(RosJsonMessage widgetContext, List<Widget> existingWidgets)
        {
            foreach (Widget existingWidget in existingWidgets)
            {
                if (existingWidget.GetID() == widgetContext.id)
                {                    
                    return false;
                }
            }

            return true;
        }

        public Widget CreateWidgetFromContext(RosJsonMessage widgetContext, List<Widget> existingWidgets)
        {
            if (IsWidgetIdUnique(widgetContext, existingWidgets) == false)
            {
                Debug.Log("duplicate ID: " + widgetContext.id + " in widget templates");
                return null;
            }

            Widget widget = null;
            
            switch (widgetContext.type)
            {
                case "Graph":
                    widget = CreateGraphWidget(widgetContext);
                    break;

                case "InspectorGraph":
                    widget = CreateInspectorGraphWidget(widgetContext);
                    break;

                case "Toastr":
                    widget = CreateToastrWidget(widgetContext);
                    break;

                /* For Later
                 
                case "Text":
                    widget = CreateToastrWidget(widgetContext);
                    break;

                case "Icon":
                    widget = CreateToastrWidget(widgetContext);
                    break;
                */

                default:
                    Debug.Log("Type not defined: " + widgetContext.type);
                    break;
            }

            return widget;
        }

        public Widget CreateGraphWidget(RosJsonMessage widgetContext)
        {
            GameObject newInstance = Instantiate(GraphPref);

            GraphView view = newInstance.AddComponent<GraphView>() as GraphView;
            GraphModel model = new GraphModel(view, widgetContext.title, WidgetUtility.BytesToColor(widgetContext.graphColor));
            view.Init(model, widgetContext.panel_id);
            GraphController controller = new GraphController(model);

            Widget widget = Manager.Instance.gameObject.AddComponent<Widget>() as Widget;
            widget.InitializeWidget(controller, model, view, widgetContext);
            return widget;
        }

        public Widget CreateInspectorGraphWidget(RosJsonMessage widgetContext)
        {
            InspectorGraphView view = gameObject.AddComponent<InspectorGraphView>() as InspectorGraphView;
            GraphModel model = new GraphModel(view, widgetContext.title, WidgetUtility.BytesToColor(widgetContext.graphColor));
            view.Init(model, widgetContext.panel_id);
            GraphController controller = new GraphController(model);
            Widget widget = Manager.Instance.gameObject.AddComponent<Widget>() as Widget;

            widget.InitializeWidget(controller, model, view, widgetContext);

            return widget;
        }

        // TODO: adjust MVC for TextBanner and implement new needed Variables (duration, ...) 
        public Widget CreateToastrWidget(RosJsonMessage widgetContext)
        {
            // TODO: Greate new MVC Classes
            //GameObject newInstance = Instantiate(GraphPref);
            View view = Manager.Instance.gameObject.AddComponent<ToastrView>() as ToastrView;
            //Model model = new TextBannerModel(view, widgetContext.pos, widgetContext.title, widgetContext.duration, widgetContext.color, widgetContext.fontSize);
            ToastrModel model = new ToastrModel(view, widgetContext.title, widgetContext.toastrDuration, WidgetUtility.BytesToColor(widgetContext.toastrColor), widgetContext.toastrFontSize);
            ToastrController controller = new ToastrController(model);
            Widget widget = Manager.Instance.gameObject.AddComponent<Widget>() as Widget;

            /*View view = WidgetManager.Instance.gameObject.AddComponent<GraphViewDummy>() as GraphViewDummy;
            Model model = new GraphModel(view, widgetContext.pos, widgetContext.color);
            Controller controller = new GraphController(model);
            Widget widget = WidgetManager.Instance.gameObject.AddComponent<Widget>() as Widget;*/

            widget.InitializeWidget(controller, model, view, widgetContext);

            return widget;
        }
    }
}