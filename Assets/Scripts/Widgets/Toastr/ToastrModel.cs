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

        public Queue<Printable> datapoints;

        public ToastrModel(View view, string title, float duration, Color color, int fontSize) : base(view, title)
        {
            // TODO: if 0 set to default value
            this.duration = ProcessInitialValue(duration, 6, false, "duration");
            this.color = color;

            this.fontSize = ProcessInitialValue(fontSize, 32, false, "fontSize");

            datapoints = new Queue<Printable>(SIZE);
        }

        /*
        public void ChangeMessageToDisplay(string newMessage)
        {
            messageToDisplay = newMessage;
        }
        */

        public void ChangeDuration(float newDuration)
        {
            duration = newDuration;
        }

        public void ChangeColor(Color newColor)
        {
            color = newColor;
        }

        public void ChangeFontSize(int newFontSize)
        {
            fontSize = newFontSize;
        }

        public void EnqueueNewMessage(string newMessage)
        {
            datapoints.Enqueue(new Printable(newMessage, duration, color, (byte)fontSize));

            // view.UpdateView(this);
        }
    }
}