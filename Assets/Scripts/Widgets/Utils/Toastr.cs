using TMPro;
using UnityEngine;

namespace Widgets
{   
    public class Toastr : MonoBehaviour
    {
        public string msg;
        public Color color;
        public int fontSize;

        private bool slerpActive = false;
        private Vector3 localSlerpStartPos;
        private Vector3 localSlerpStopPos;
        private float timer;
        private float duration = 1.0f;

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

        public void SlerpUp(float offsetInY, float timeOffset)
        {
            localSlerpStartPos = transform.localPosition;
            localSlerpStopPos = transform.localPosition + new Vector3(0, offsetInY, 0);

            timer = 0 - timeOffset;
            slerpActive = true;
        }

        public void Update()
        {
            if (slerpActive)
            {
                timer += Time.deltaTime;

                transform.localPosition = Vector3.Slerp(localSlerpStartPos, localSlerpStopPos, timer/duration);

                if (timer >= duration)
                {
                    slerpActive = false;
                }
            }
        }
    }
}