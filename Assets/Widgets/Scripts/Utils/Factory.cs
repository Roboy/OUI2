using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{
    public class Factory : Singleton<Factory>
    {
        public GameObject toastrDesignPrefab;
        public GameObject textDesignPrefab;
        public GameObject iconDesignPrefab;
        public GameObject graphDesignPrefab;

        readonly int TASKBAR_ICON_OFFSET = 200;

        // public CurvedUI.CurvedUISettings curvedUI;

        // public GameObject canvas;
        private GameObject widgetParentGameObject;

        Dictionary<string, Texture2D> icons;

        List<Widget> taskbarWidgets;

        // Start is called before the first frame update
        public List<Widget> CreateWidgetsAtStartup()
        {
            icons = FetchAllIcons();

            taskbarWidgets = new List<Widget>();

            CreateWidgetParentGameObject();

            List<RosJsonMessage> widgetContexts = TemplateParser.ParseAllWidgetTemplates();

            List<Widget> widgets = new List<Widget>();

            foreach (RosJsonMessage widgetContext in widgetContexts)
            {
                Widget createdWidget = CreateWidgetFromContext(widgetContext, widgets);

                if (createdWidget == null)
                {
                    Debug.LogWarning("widget is null");
                    continue;
                }

                widgets.Add(createdWidget);
            }

            // PositionTaskbarWidgets();

            return widgets;
        }
        
        private void CreateWidgetParentGameObject()
        {
            widgetParentGameObject = new GameObject("Widgets");
        }

        private Dictionary<string, Texture2D> FetchAllIcons()
        {
            Texture2D[] iconsArray = Resources.LoadAll<Texture2D>("Icons");

            Dictionary<string, Texture2D> iconsDictionary = new Dictionary<string, Texture2D>();

            foreach (Texture2D icon in iconsArray)
            {
                iconsDictionary.Add(icon.name, icon);
            }

            return iconsDictionary;
        }

        private Dictionary<string, Texture2D> FindIconsWithName(string[] iconNames)
        {
            Dictionary<string, Texture2D> iconsFound = new Dictionary<string, Texture2D>();

            foreach (string name in iconNames)
            {
                if (icons.ContainsKey(name))
                {
                    Texture2D iconFound;
                    icons.TryGetValue(name, out iconFound);
                    iconsFound.Add(name, iconFound);
                }
            }

            return iconsFound;
        }

        // TODO: What is the performance of this call?
        private void LateUpdate()
        {
            // curvedUI.AddEffectToChildren();
        }

        /*
        private void PositionTaskbarWidgets()
        {           
            for (int i = 0; i < taskbarWidgets.Count; i++)
            {
                taskbarWidgets[i].transform.position = panelTransforms[0].transform.position;

                taskbarWidgets[i].transform.localPosition += (Vector3.right * (taskbarWidgets.Count - 1) * (TASKBAR_ICON_OFFSET / 2) * -1) + Vector3.right * TASKBAR_ICON_OFFSET * i;
            }
        }
        */

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
                Debug.LogWarning("duplicate ID: " + widgetContext.id + " in widget templates");
                return null;
            }

            GameObject widgetGameObject = new GameObject();
            widgetGameObject.name = widgetContext.title;
            widgetGameObject.transform.SetParent(widgetParentGameObject.transform, false);

            switch (widgetContext.type)
            {
                case "Graph":
                    GraphWidget graphWidget = widgetGameObject.AddComponent<GraphWidget>();
                    graphWidget.Init(widgetContext, graphDesignPrefab);
                    return graphWidget;

                case "Toastr":
                    ToastrWidget toastrWidget = widgetGameObject.AddComponent<ToastrWidget>();
                    toastrWidget.Init(widgetContext, toastrDesignPrefab);
                    return toastrWidget;

                case "Icon":
                    IconWidget iconWidget = widgetGameObject.AddComponent<IconWidget>();
                    Dictionary<string, Texture2D> iconsForThisWidget = FindIconsWithName(widgetContext.icons);
                    iconWidget.Init(widgetContext, iconDesignPrefab, iconsForThisWidget);
                    taskbarWidgets.Add(iconWidget);                    
                    return iconWidget;
                    
                case "Text":
                    TextWidget textWidget = widgetGameObject.AddComponent<TextWidget>();
                    textWidget.Init(widgetContext, textDesignPrefab);
                    return textWidget;

                default:
                    Debug.LogWarning("Type not defined: " + widgetContext.type);
                    Destroy(widgetGameObject);
                    return null;
            }
        }
    }
}