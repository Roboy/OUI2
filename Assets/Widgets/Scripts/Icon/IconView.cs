using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Widgets
{
    public class IconView : View
    {
        private RawImage image;
        
        public void SetIcon(Texture2D iconTexture)
        {
            image.texture = iconTexture;
        }

        public override void ShowView(RelativeChildPosition relativeChildPosition)
        {
            image.enabled = true;
        }

        public override void HideView()
        {
            image.enabled = false;
        }

        public override void Init(Widget widget)
        {
            GameObject viewDesign = Instantiate(widget.viewDesignPrefab);
            viewDesign.transform.SetParent(this.transform, false);
            image = gameObject.GetComponentInChildren<RawImage>();

            SetChildWidget(widget.childWidget);            
            SetIcon(((IconWidget)widget).currentIcon);

            Init(widget.relativeChildPosition);
        }
    }
}