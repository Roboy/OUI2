using TMPro;
using UnityEngine;

namespace Widgets
{   
    public class Toastr : MonoBehaviour
    {
        public string msg;
        public Color color;
        public int fontSize;

        TextMeshProUGUI textMeshPro;

        public void Init(string msg, Color color, int fontSize)
        {
            this.msg = msg;
            this.color = color;
            this.fontSize = fontSize;

            textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();
            textMeshPro.SetText(msg);
            textMeshPro.fontSize = fontSize;
            textMeshPro.color = color;
        }
    }
}