using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{   
    public class Toastr : MonoBehaviour
    {
        public string msg;
        public float duration;
        public Color color;
        public int fontSize;

        TextMeshProUGUI textMeshPro;

        public void Init(string msg, float duration, Color color, int fontSize)
        {
            this.msg = msg;
            this.duration = duration;
            this.color = color;
            this.fontSize = fontSize;

            textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();
            textMeshPro.SetText(msg);
            textMeshPro.fontSize = fontSize;
            textMeshPro.color = color;
        }
    }
}