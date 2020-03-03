using UnityEngine;
using UnityEngine.EventSystems;

namespace Widgets
{
    public abstract class View : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Widget childWidget;
        public View parentView;

        public float timer;
        float duration = 0.5f;

        public bool timerActive = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            timer = 0;
            timerActive = false;

            if (parentView != null)
            {
                parentView.OnPointerEnter(eventData);
            }

            if (childWidget != null)
            {
                childWidget.GetView().SetParentView(this);
                childWidget.GetView().ShowView();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            timer = 0;
            timerActive = true;
        }

        private void TimeIsUp()
        {
            if (parentView != null)
            {
                parentView.OnPointerExit(null);
            }

            if (childWidget != null)
            {
                childWidget.GetView().SetParentView(null);
                childWidget.GetView().HideView();
            }

            timerActive = false;
        }

        public abstract void ShowView();

        public abstract void HideView();

        public void SetChildWidget(Widget childWidget)
        {
            this.childWidget = childWidget;
        }

        public void SetParentView(View parentView)
        {
            this.parentView = parentView;
        }

        public void Update()
        {
            if (timerActive)
            {
                timer += Time.deltaTime;

                if (timer >= duration)
                {
                    TimeIsUp();
                }
            }
        }
    }
}
