using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{   
    public class Toastr : MonoBehaviour
    {
        public string msg;
        public Color color;
        public int fontSize;

        private readonly float SLERP_DURATION = 0.5f;

        private bool slerpActive = false;
        private Vector3 localSlerpStartPos;
        private Vector3 localSlerpStopPos;
        private Timer timer;

        TextMeshProUGUI textMeshPro;

        public void Init(string msg, Color color, int fontSize)
        {
            this.msg = msg;
            this.color = color;
            this.fontSize = fontSize;

            // textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();

            textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.SetText(msg);
            textMeshPro.fontSize = fontSize;
            textMeshPro.color = color;

            timer = new Timer();

            gameObject.AddComponent<CurvedUI.CurvedUIVertexEffect>();
        }

        public void SlerpUp(float offsetInY, float timeOffset)
        {
            localSlerpStartPos = transform.localPosition;
            localSlerpStopPos = transform.localPosition + new Vector3(0, offsetInY, 0);
            timer.SetTimer(SLERP_DURATION + timeOffset, StopSlerp);
            slerpActive = true;
        }

        public void Update()
        {
            if (slerpActive)
            {
                timer.LetTimePass(Time.deltaTime);

                transform.localPosition = Vector3.Slerp(localSlerpStartPos, localSlerpStopPos, timer.GetFraction());
            }
        }

        public void StopSlerp()
        {
            slerpActive = false;
        }
    }
}