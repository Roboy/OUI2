using UnityEngine;

namespace Widgets
{
    public abstract class Widget : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private int panelId;

        private RosJsonMessage context;
        public int childWidgetId;
        public Widget childWidget;

        protected bool isChildWidget = false;

        public void Init(RosJsonMessage RosJsonMessage)
        {
            context = RosJsonMessage;
            id = RosJsonMessage.id;
            childWidgetId = RosJsonMessage.childWidgetId;
            panelId = RosJsonMessage.panelId;
        }

        public int GetID()
        {
            return id;
        }

        public int GetPanelID()
        {
            return panelId;
        }

        public RosJsonMessage GetContext()
        {
            return context;
        }

        public void SetIsChildWidget()
        {
            isChildWidget = true;
        }

        private void Update()
        {
            if (childWidget == null && childWidgetId != 0)
            {
                childWidget = Manager.Instance.FindWidgetWithID(childWidgetId);

                childWidget.SetIsChildWidget();

                if (childWidget == null)
                {
                    Debug.LogWarning("Child widget not found.");
                }
            }

            UpdateInClass();
        }

        protected abstract void UpdateInClass();

        public abstract void ProcessRosMessage(RosJsonMessage rosMessage);

        public abstract void RestoreViews(GameObject viewParent);

        public abstract View GetView();
    }
}
