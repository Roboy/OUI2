using UnityEngine;
using UnityEngine.EventSystems;

namespace Widgets
{
    public abstract class View : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Widget childWidget;
        public View parentView;

        private RelativeChildPosition relativeChildPosition;

        private Timer timer;
        // float duration = 0.5f;

        public bool timerActive = false;

        public abstract void Init(Widget widget);

        public void Init(RelativeChildPosition relativeChildPosition)
        {
            // AttachCurvedUI();
            SetRelativeChildPosition(relativeChildPosition);
            timer = new Timer();
        }

        public void OnSelectionEnter()
        {
            timer.SetTimer(0.5f, TimeIsUp);
            timerActive = false;

            print("ENTER");

            if (parentView != null)
            {
                parentView.OnSelectionEnter();
            }

            if (childWidget != null)
            {
                childWidget.GetView().SetParentView(this);
                childWidget.GetView().ShowView(relativeChildPosition);
            }
        }

        public void AttachCurvedUI()
        {
            gameObject.AddComponent<CurvedUI.CurvedUIVertexEffect>();
        }

        public void SetRelativeChildPosition(RelativeChildPosition relativeChildPosition)
        {
            this.relativeChildPosition = relativeChildPosition;
        }

        public void OnSelectionExit()
        {
            timer.ResetTimer();
            timerActive = true;
        }

        private void TimeIsUp()
        {
            if (parentView != null)
            {
                parentView.OnSelectionExit();
            }

            if (childWidget != null)
            {
                childWidget.GetView().SetParentView(null);
                childWidget.GetView().HideView();
            }

            timerActive = false;
        }

        public abstract void ShowView(RelativeChildPosition relativeChildPosition);

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
                timer.LetTimePass(Time.deltaTime);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnSelectionEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnSelectionExit();
        }
    }
}
