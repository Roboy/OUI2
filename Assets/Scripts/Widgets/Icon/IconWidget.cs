﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Widgets
{
    public class IconWidget : Widget
    {
        public Dictionary<string, Texture2D> icons;
        public Texture2D[] iconsArray;
        
        private string currentIconName;
        private Texture2D currentIcon;

        IconView view;

        public override View GetView()
        {
            return view;
        }

        public void Init(RosJsonMessage context, Dictionary<string, Texture2D> icons)
        {
            this.icons = icons;
            iconsArray = new Texture2D[icons.Count];
            icons.Values.CopyTo(iconsArray, 0);

            childWidgetId = context.childWidgetId;

            ProcessRosMessage(context);

            base.Init(context);
        }

        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            if (!rosMessage.currentIcon.Equals(""))
            {
                currentIconName = rosMessage.currentIcon;
                if (icons.TryGetValue(currentIconName, out currentIcon))
                {
                    if (view != null)
                    {
                        view.SetIcon(currentIcon);
                    }
                }
            }
        }

        public override void CreateView(GameObject iconParent)
        {
            GameObject iconGameObject = new GameObject();
            iconGameObject.transform.SetParent(iconParent.transform, false);
            iconGameObject.name = gameObject.name + "View";
            view = iconGameObject.AddComponent<IconView>();
            view.Init(currentIcon, childWidget != null ? childWidget : null);

            if (isChildWidget)
            {
                view.HideView();
            }
        }

        protected override void UpdateInClass()
        {

        }
    }
}