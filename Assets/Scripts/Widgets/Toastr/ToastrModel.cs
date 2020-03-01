using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class ToastrModel : Model
    {
        const int SIZE = 100;

        public float duration;
        public Color color;
        public int fontSize;

        public string messageToDisplay;

        public Queue<Toastr> toastrQueue;

        public void Awake()
        {
            toastrQueue = new Queue<Toastr>(SIZE);
        }

        public void Init(View view, string title, float duration, Color color, int fontSize)
        {
            base.Init(view, title);

            this.color = color;
        }

        public void EnqueueNewMessage(string toastrMessage, float toastrDuration, Color toastrColor, int toastrFontSize)
        {
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

            if (toastrMessage.Equals(""))
            {
                return;
            }

            GameObject toastrGameObject = new GameObject();
            toastrGameObject.transform.SetParent(this.transform, false);
            toastrGameObject.name = toastrMessage;
            Toastr newToastr = toastrGameObject.gameObject.AddComponent<Toastr>();
            newToastr.Init(toastrMessage, toastrDuration, toastrColor, toastrFontSize);

            toastrQueue.Enqueue(newToastr);
            ((ToastrView)view).NewToastr();
        }

        public void Update()
        {
            if (toastrQueue.Count != 0)
            {
                toastrQueue.Peek().duration -= Time.deltaTime;

                if (toastrQueue.Peek().duration <= 0.0f)
                {
                    ((ToastrView)view).DeleteToastr(toastrQueue.Dequeue());
                }
            }
        }
    }
}