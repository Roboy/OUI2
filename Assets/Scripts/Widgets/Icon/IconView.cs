using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Widgets
{
    public class IconView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RawImage image;

        private void Awake()
        {
            image = gameObject.AddComponent<RawImage>();
        }

        public void Init(Texture2D icon)
        {   
            SetIcon(icon);
        }

        public void SetIcon(Texture2D iconTexture)
        {
            image.texture = iconTexture;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("POIMT");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}