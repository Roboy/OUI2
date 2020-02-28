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

        public ToastrModel(View view, string title, float duration, Color color, int fontSize) : base(view, title)
        {
            // TODO: if 0 set to default value
            this.duration = ProcessInitialValue(duration, 6, false, "duration");
            this.color = color;

            this.fontSize = ProcessInitialValue(fontSize, 32, false, "fontSize");

            toastrQueue = new Queue<Toastr>(SIZE);
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

            toastrQueue.Enqueue(new Toastr(toastrMessage, toastrDuration, toastrColor, toastrFontSize));
            ((ToastrView)view).NewToastr();
        }

        public void Update()
        {
            toastrQueue.Peek().duration -= Time.deltaTime;

            if (toastrQueue.Peek().duration <= 0.0f)
            {
                toastrQueue.Dequeue();
                ((ToastrView)view).DeleteTopToastr();
            }
        }
    }
}