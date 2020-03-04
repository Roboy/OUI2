﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Widgets
{
    public class IconView : View
    {
        private RawImage image;

        private void Awake()
        {
            image = gameObject.AddComponent<RawImage>();
        }

        public void SetIcon(Texture2D iconTexture)
        {
            image.texture = iconTexture;
        }

        public override void ShowView()
        {
            image.enabled = true;
        }

        public override void HideView()
        {
            image.enabled = false;
        }

        public override void Init(Widget widget)
        {
            SetChildWidget(widget.childWidget);
            AttachCurvedUI();
            SetIcon(((IconWidget)widget).currentIcon);
        }
    }
}