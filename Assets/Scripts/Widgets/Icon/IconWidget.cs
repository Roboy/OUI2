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
        private RawImage image;


        public void Awake()
        {
            image = gameObject.AddComponent<RawImage>();
        }

        public new void Init(RosJsonMessage rosJsonMessage, Dictionary<string, Texture2D> icons)
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
                    image.texture = currentIcon;
                }
            }
        }

        public void Update()
        {
            if (childWidget == null)
            {
                childWidget = Manager.Instance.FindWidgetWithID(childWidgetId);
            }
        }
    }
}