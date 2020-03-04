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

        public void Init(RosJsonMessage context)
        {
            this.context = context;
            id = context.id;
            childWidgetId = context.childWidgetId;
            panelId = context.panelId;
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

        private void SearchAndSetChild()
        {
            childWidget = Manager.Instance.FindWidgetWithID(childWidgetId);

            childWidget.SetIsChildWidget();

            if (childWidget == null)
            {
                Debug.LogWarning("Child widget not found.");
            }
        }

        private void Update()
        {
            if (childWidget == null && childWidgetId != 0)
            {
                SearchAndSetChild();
            }
            
            // This is true, everytime the HUD scene changes
            if (GetView() == null)
            {
                RecreateView();
            }

            UpdateInClass();
        }

        protected abstract void UpdateInClass();

        public abstract void ProcessRosMessage(RosJsonMessage rosMessage);

        public abstract void CreateView(GameObject viewParent);

        private void RecreateView()
        {
            GameObject textParent = GameObject.FindGameObjectWithTag("Panel_" + GetPanelID());
            if (textParent != null)
            {
                CreateView(textParent);
            }
        }

        public abstract View GetView();
    }
}
