﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{
    public class Factory : Singleton<Factory>
    {
        readonly int TASKBAR_ICON_OFFSET = 200;

        public GameObject GraphPref;
        public CurvedUI.CurvedUISettings curvedUI;

        public GameObject canvas;
        private GameObject widgetParentGameObject;

        Dictionary<string, Texture2D> icons;

        public Transform[] panelTransforms;

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
                    Debug.Log("widget is null");
                    continue;
                }

                widgets.Add(createdWidget);
            }

            PositionTaskbarWidgets();

            return widgets;
        }
        
        private void CreateWidgetParentGameObject()
        {
            widgetParentGameObject = new GameObject();
            widgetParentGameObject.transform.SetParent(canvas.transform, false);
            widgetParentGameObject.name = "Widgets";
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
            curvedUI.AddEffectToChildren();
        }

        private void PositionTaskbarWidgets()
        {
            

            for (int i = 0; i < taskbarWidgets.Count; i++)
            {
                taskbarWidgets[i].transform.position = panelTransforms[0].transform.position;

                taskbarWidgets[i].transform.localPosition += (Vector3.right * (taskbarWidgets.Count - 1) * (TASKBAR_ICON_OFFSET / 2) * -1) + Vector3.right * TASKBAR_ICON_OFFSET * i;
            }
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

            GameObject widgetGameObject = new GameObject();
            widgetGameObject.name = widgetContext.title;
            widgetGameObject.transform.SetParent(widgetParentGameObject.transform, false);

            switch (widgetContext.type)
            {
                case "Graph":
                    GraphWidget graphWidget = widgetGameObject.AddComponent<GraphWidget>();
                    graphWidget.Init(widgetContext);
                    return graphWidget;

                case "Toastr":
                    ToastrWidget toastrWidget = widgetGameObject.AddComponent<ToastrWidget>();
                    toastrWidget.Init(widgetContext);
                    return toastrWidget;

                /* For Later
                 
                case "Text":
                    widget = CreateToastrWidget(widgetContext);
                    break;
                */

                case "Icon":
                    IconWidget iconWidget = widgetGameObject.AddComponent<IconWidget>();
                    Dictionary<string, Texture2D> iconsForThisWidget = FindIconsWithName(widgetContext.icons);
                    iconWidget.Init(widgetContext, iconsForThisWidget);
                    taskbarWidgets.Add(iconWidget);                    
                    return iconWidget;

                default:
                    Debug.Log("Type not defined: " + widgetContext.type);
                    Destroy(widgetGameObject);
                    return null;
            }
        }
    }
}