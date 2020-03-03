using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Widgets
{
    public class IconWidget : Widget, IPointerEnterHandler, IPointerExitHandler
    {
        public Dictionary<string, Texture2D> icons;
        public Texture2D[] iconsArray;

        private int childWidgetId;
        Widget childWidget;

        private string currentIconName;
        private Texture2D currentIcon;

        IconView view;

        public void Init(RosJsonMessage rosJsonMessage, Dictionary<string, Texture2D> icons)
        {
            this.icons = icons;
            iconsArray = new Texture2D[icons.Count];
            icons.Values.CopyTo(iconsArray, 0);

            childWidgetId = rosJsonMessage.childWidgetId;

            ProcessRosMessage(rosJsonMessage);

            base.Init(rosJsonMessage);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("POIMT");

            childWidget.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            childWidget.gameObject.SetActive(false);
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

        public override void RestoreViews(GameObject iconParent)
        {
            GameObject iconGameObject = new GameObject();
            iconGameObject.transform.SetParent(iconParent.transform, false);
            view = iconGameObject.AddComponent<IconView>();
            view.Init(currentIcon);
        }

        public void Update()
        {
            if (childWidget == null)
            {
                childWidget = Manager.Instance.FindWidgetWithID(childWidgetId);
                
                if (childWidget == null)
                {
                    Debug.LogWarning("Child widget not found.");
                }
            }

            // This is true, everytime the HUD scene changes
            if (view == null && currentIcon != null)
            {
                GameObject iconParent = GameObject.FindGameObjectWithTag("Panel_" + GetPanelID());
                
                if (iconParent != null)
                {
                    RestoreViews(iconParent);
                }
            }
        }
    }
}