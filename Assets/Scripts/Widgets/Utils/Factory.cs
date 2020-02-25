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
            List<Context> widgetContexts = TemplateParser.ParseAllWidgetTemplates();

            List<Widget> widgets = new List<Widget>();

            foreach (Context widgetContext in widgetContexts)
            {
                Widget createdWidget = CreateWidgetFromContext(widgetContext, widgets);

                if (createdWidget == null)
                {
                    Debug.Log("null");
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

        private bool IsWidgetIdUnique(Context widgetContext, List<Widget> existingWidgets)
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

        public Widget CreateWidgetFromContext(Context widgetContext, List<Widget> existingWidgets)
        {
            if (IsWidgetIdUnique(widgetContext, existingWidgets) == false)
            {
                return null;
            }

            Widget widget = null;
            // Debug.Log(widgetContext.type.ToString());

            switch (widgetContext.type)
            {
                case "Graph":
                    widget = CreateGraphWidget(widgetContext);
                    break;

                case "InspectorGraph":
                    widget = CreateInspectorGraphWidget(widgetContext);
                    break;

                case "TextBanner":
                    widget = CreateTextBannerWidget(widgetContext);
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

        public Widget CreateGraphWidget(Context widgetContext)
        {
            GameObject newInstance = Instantiate(GraphPref);
            View view = newInstance.AddComponent<GraphView>() as GraphView;
            Model model = new GraphModel(view, widgetContext.pos, widgetContext.title, widgetContext.detailedPanelPos, WidgetUtility.BytesToColor(widgetContext.color));
            view.Init(model);
            Controller controller = new GraphController(model);
            Widget widget = Manager.Instance.gameObject.AddComponent<Widget>() as Widget;

            /*View view = WidgetManager.Instance.gameObject.AddComponent<GraphViewDummy>() as GraphViewDummy;
            Model model = new GraphModel(view, widgetContext.pos, widgetContext.color);
            Controller controller = new GraphController(model);
            Widget widget = WidgetManager.Instance.gameObject.AddComponent<Widget>() as Widget;*/

            widget.InitializeWidget(controller, model, view, widgetContext);

            return widget;
        }

        public Widget CreateInspectorGraphWidget(Context widgetContext)
        {
            View view = gameObject.AddComponent<InspectorGraphView>() as InspectorGraphView;
            Model model = new GraphModel(view, widgetContext.pos, widgetContext.title, widgetContext.detailedPanelPos, WidgetUtility.BytesToColor(widgetContext.color));
            view.Init(model);
            Controller controller = new GraphController(model);
            Widget widget = Manager.Instance.gameObject.AddComponent<Widget>() as Widget;

            widget.InitializeWidget(controller, model, view, widgetContext);

            return widget;
        }

        // TODO: adjust MVC for TextBanner and implement new needed Variables (duration, ...) 
        public Widget CreateTextBannerWidget(Context widgetContext)
        {
            // TODO: Greate new MVC Classes
            //GameObject newInstance = Instantiate(GraphPref);
            View view = Manager.Instance.gameObject.AddComponent<TextBannerView>() as TextBannerView;
            //Model model = new TextBannerModel(view, widgetContext.pos, widgetContext.title, widgetContext.duration, widgetContext.color, widgetContext.fontSize);
            Model model = new TextBannerModel(view, widgetContext.pos, widgetContext.title, widgetContext.duration, WidgetUtility.BytesToColor(widgetContext.color), widgetContext.fontSize);
            Controller controller = new TextBannerController(model);
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