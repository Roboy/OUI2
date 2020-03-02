using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class ToastrWidget : Widget
    {
        public readonly int OFFSET = 40;

        public Queue<Toastr> toastrQueue;

        public float duration;
        public Color color;
        public int fontSize;

        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            EnqueueNewMessage(rosMessage.toastrMessage, rosMessage.toastrDuration, WidgetUtility.BytesToColor(rosMessage.toastrColor), rosMessage.toastrFontSize);
        }

        private void Awake()
        {
            toastrQueue = new Queue<Toastr>();
        }

        public new void Init(RosJsonMessage rosJsonMessage)
        {
            duration = rosJsonMessage.toastrDuration;
            color = WidgetUtility.BytesToColor(rosJsonMessage.toastrColor);
            fontSize = rosJsonMessage.toastrFontSize;
            
            base.Init(rosJsonMessage);
        }

        private void EnqueueNewMessage(string toastrMessage, float toastrDuration, Color toastrColor, int toastrFontSize)
        {
            if (toastrMessage.Equals(""))
            {
                return;
            }

            if (toastrColor == null)
            {
                toastrColor = color;
            }

            if (toastrDuration == 0.0f)
            {
                toastrDuration = duration;
            }

            if (toastrFontSize == 0)
            {
                toastrFontSize = fontSize;
            }

            toastrQueue.Enqueue(CreateNewToastr(toastrMessage, toastrDuration, toastrColor, toastrFontSize));
        }

        private Toastr CreateNewToastr(string toastrMessage, float toastrDuration, Color toastrColor, int toastrFontSize)
        {
            GameObject toastrGameObject = new GameObject();
            toastrGameObject.transform.SetParent(this.transform, false);
            toastrGameObject.transform.localPosition -= toastrQueue.Count * OFFSET * Vector3.up;

            toastrGameObject.name = toastrMessage;
            Toastr newToastr = toastrGameObject.gameObject.AddComponent<Toastr>();
            newToastr.Init(toastrMessage + " " + toastrQueue.Count, toastrDuration, toastrColor, toastrFontSize);

            return newToastr;
        }

        private void MoveToastrsUp()
        {
            foreach (Toastr toastr in toastrQueue)
            {
                toastr.transform.localPosition += OFFSET * Vector3.up;
            }
        }

        public void Update()
        {
            if (toastrQueue.Count != 0)
            {
                toastrQueue.Peek().duration -= Time.deltaTime;

                if (toastrQueue.Peek().duration <= 0.0f)
                {
                    Destroy(toastrQueue.Dequeue().gameObject);
                    MoveToastrsUp();
                }
            }
        }
    }
}
