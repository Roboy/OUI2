using UnityEngine;

namespace Widgets
{
    public abstract class Widget : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private WidgetPosition position;

        public GameObject viewDesignPrefab;

        private RosJsonMessage context;
        public int childWidgetId;
        public Widget childWidget;
        public RelativeChildPosition relativeChildPosition;
        
        protected View view;

        public void Init(RosJsonMessage context, GameObject viewDesignPrefab)
        {
            this.context = context;
            id = context.id;
            childWidgetId = context.childWidgetId;
            position = WidgetUtility.StringToWidgetPosition(context.widgetPosition);
            relativeChildPosition = WidgetUtility.StringToRelativeChildPosition(context.relativeChildPosition);

            this.viewDesignPrefab = viewDesignPrefab;
        }

        public int GetID()
        {
            return id;
        }

        public WidgetPosition GetWidgetPosition()
        {
            return position;
        }

        public RosJsonMessage GetContext()
        {
            return context;
        }

        private void SearchAndSetChild()
        {
            childWidget = Manager.Instance.FindWidgetWithID(childWidgetId);

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
            
            // This is true, everytime the HUD is destroyed
            if (GetView() == null)
            {
                TryToRecreateView();
            }

            UpdateInClass();
        }

        protected abstract void UpdateInClass();

        public abstract void ProcessRosMessage(RosJsonMessage rosMessage);

        public void CreateView(GameObject viewParent)
        {
            GameObject viewGameObject = new GameObject(gameObject.name + "View", typeof(RectTransform));
            viewGameObject.transform.SetParent(viewParent.transform, false);
            // viewGameObject.name = gameObject.name + "View";
            view = AddViewComponent(viewGameObject);
            view.Init(this);

            if (position == WidgetPosition.Child)
            {
                view.HideView();
            }
        }

        private void TryToRecreateView()
        {
            GameObject textParent = GameObject.FindGameObjectWithTag("Widgets" + GetWidgetPosition());
            if (textParent != null)
            {
                CreateView(textParent);
            }
        }

        public View GetView()
        {
            return view;
        }

        public abstract View AddViewComponent(GameObject viewGameObject);
    }
}
