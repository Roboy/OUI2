using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBannerModel : Model
{
    const int SIZE = 100;

    public float duration;
    public int color;
    public int fontSize;

    public string messageToDisplay;

    public Queue<Printable> datapoints;

    public TextBannerModel(View view, int pos, string title, float duration, int color, int fontSize) : base(view, pos, title)
    {
        // TODO: if 0 set to default value
        this.duration = ProcessInitialValue(duration, 6, false, "duration");
        this.color = color;

        this.fontSize = ProcessInitialValue(fontSize, 32, false, "fontSize");

        datapoints = new Queue<Printable>(SIZE);
    }

    public override void UpdateModel(WidgetMessage newMessage)
    {
        TextBannerMessage newTextBannerMessage = (TextBannerMessage)newMessage;

        if (newTextBannerMessage.msg != "")
        {
            messageToDisplay = newTextBannerMessage.msg;
        }

        if (newTextBannerMessage.duration != 0)
        {
            duration = newTextBannerMessage.duration;
        }

        if (newTextBannerMessage.color != 0)
        {
            color = newTextBannerMessage.color;
        }

        if (newTextBannerMessage.pos != 0)
        {
            pos = newTextBannerMessage.pos;
        }

        if (newTextBannerMessage.fontSize != 0)
        {
            fontSize = newTextBannerMessage.fontSize;
        }

        datapoints.Enqueue(new Printable(newTextBannerMessage.msg, duration, ParseColor(color), (byte)fontSize));

        view.UpdateView(this);
    }

    // TODO: same colors for each type??
    public Color ParseColor(int c)
    {
        return Color.red;
    }
}
