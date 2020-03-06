using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Widgets
{
    public abstract class View : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public float keepOpenDuration = 0.5f;
        public float dwellTimerDuration;

        public Widget childWidget;
        public View parentView;
        public Image dwellTimerImage;

        private RelativeChildPosition relativeChildPosition;

        private Timer keepChildUnfoldedTimer;
        private Timer dwellTimer;

        public bool keepChildUnfolded = false;
        public bool dwellTimerActive = false;

        public abstract void Init(Widget widget);

        #region FLAGS
        public bool isLookedAt = false;
        public bool childIsActive = false;
        public bool useDwellTimer = false;
        #endregion

        public void Init(RelativeChildPosition relativeChildPosition, float dwellTimerDuration)
        {
            SetRelativeChildPosition(relativeChildPosition);
            this.dwellTimerDuration = dwellTimerDuration;

            if (dwellTimerDuration > 0)
            {
                useDwellTimer = true;
            }

            dwellTimerImage = gameObject.GetComponentInChildren<Image>();
            
            keepChildUnfoldedTimer = new Timer();
            dwellTimer = new Timer();
        }

        public void UnfoldChild()
        {
            childIsActive = true;

            /*
            if (parentView != null)
            {
                parentView.OnSelectionEnter();
            }
            */

            if (childWidget != null)
            {
                childWidget.GetView().SetParentView(this);
                childWidget.GetView().ShowView(relativeChildPosition);
            }

            dwellTimerActive = false;
        }

        public void FoldChildIn()
        {
            childIsActive = false;

            if (parentView != null)
            {
                parentView.OnSelectionExit();
                ResetDwellTimer();
            }

            if (childWidget != null)
            {
                childWidget.GetView().SetParentView(null);
                childWidget.GetView().HideView();
            }

            keepChildUnfolded = false;
        }

        public void ResetDwellTimer()
        {
            dwellTimer.ResetTimer();
            dwellTimerActive = false;
        }

        public void OnSelectionChildEnter()
        {
            keepChildUnfolded = false;
        }

        public void OnSelectionChildExit()
        {
            keepChildUnfolded = true;
        }

        public void OnSelectionEnter()
        {
            isLookedAt = true;

            keepChildUnfoldedTimer.SetTimer(keepOpenDuration, FoldChildIn);
            keepChildUnfolded = false;

            if (parentView != null)
            {
                parentView.OnSelectionChildEnter();
            }

            if (useDwellTimer)
            {
                dwellTimer.SetTimer(dwellTimerDuration, UnfoldChild);
                dwellTimerActive = true;
            }
            else
            {
                UnfoldChild();
            }
        }

        public void OnSelectionExit()
        {
            isLookedAt = false;

            if (parentView != null)
            {
                OnSelectionChildExit();
            }

            keepChildUnfoldedTimer.ResetTimer();
            keepChildUnfolded = true;

            dwellTimerActive = false;
            dwellTimerImage.fillAmount = 0.0f;
        }

        public void SetRelativeChildPosition(RelativeChildPosition relativeChildPosition)
        {
            this.relativeChildPosition = relativeChildPosition;
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
            // Folding child in again timer
            if (keepChildUnfolded)
            {
                keepChildUnfoldedTimer.LetTimePass(Time.deltaTime);
            }

            // Fold child out dwell timer
            if (isLookedAt && useDwellTimer)
            {
                dwellTimer.LetTimePass(Time.deltaTime);

                dwellTimerImage.fillAmount = dwellTimer.GetFraction();
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
