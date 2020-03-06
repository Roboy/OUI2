using System;

namespace Widgets
{
    [Serializable]
    public class RosJsonMessage
    {
        
        public int id;  // MANDATORY

        #region General        
        public string title;
        public string type;
        public string widgetPosition;
        public string relativeChildPosition;
        public float unfoldChildDwellTimer;
        public int childWidgetId;
        public int timestamp;
        #endregion

        #region Graph
        // Time passed in ms since 1/1/1970
        public double graphTimestamp;  // MANDATORY FOR GRAPH
        public float graphValue;    // MANDATORY FOR GRAPH
        public byte[] graphColor;
        public string graphTitle;
        public int xDivisionUnits;
        public int yDivisionUnits;
        public bool showCompleteHistory;
        #endregion

        #region Toastr
        public string toastrMessage;    // MANDATORY FOR GRAPH
        public int toastrFontSize;
        public byte[] toastrColor;
        public float toastrDuration;
        #endregion

        #region Text
        public string textMessage;      // MANDATORY FOR GRAPH
        public int textFontSize;
        public byte[] textColor;
        #endregion

        #region Icon
        public string currentIcon;      // MANDATORY FOR GRAPH
        public string[] icons;
        #endregion
        

        private RosJsonMessage(int id, float graphDatapoint, double graphTimestamp, byte[] graphColor)
        {
            this.id = id;
            this.graphValue = graphDatapoint;
            this.graphColor = graphColor;
            this.graphTimestamp = graphTimestamp;
        }

        private RosJsonMessage(int id, string currentIcon)
        {
            this.id = id;
            this.currentIcon = currentIcon;
        }

        private RosJsonMessage(int id, string toastrMessage, float toastrDuration, byte[] col)
        {
            this.id = id;
            this.toastrMessage = toastrMessage;
            this.toastrDuration = toastrDuration;
            this.toastrColor = col;
        }

        public RosJsonMessage(int id, string title, string type, string widgetPosition, string relativeChildPosition, int childWidgetId, string textMessage, int textFontSize, byte[] textColor) : this(id, title)
        {
            this.type = type;
            this.widgetPosition = widgetPosition;
            this.relativeChildPosition = relativeChildPosition;
            this.childWidgetId = childWidgetId;
            this.textMessage = textMessage;
            this.textFontSize = textFontSize;
            this.textColor = textColor;
        }

        public static RosJsonMessage CreateTextMessage(int id, string title, string type, string widgetPosition,
            string relativeChildPosition, int childWidgetId, string textMessage, int textFontSize, byte[] textColor)
        {
            return new RosJsonMessage(id, title, type, widgetPosition, relativeChildPosition,
                childWidgetId, textMessage, textFontSize, textColor);
        }

            public static RosJsonMessage CreateGraphMessage(int id, float graphDatapoint, double timestamp, byte[] graphColor)
        {
            return new RosJsonMessage(id, graphDatapoint, timestamp, graphColor);
        }

        public static RosJsonMessage CreateIconMessage(int id, string currentIcon)
        {
            return new RosJsonMessage(id, currentIcon);
        }

        public static RosJsonMessage CreateToastrMessage(int id, string text, float duration, byte[] col)
        {
            return new RosJsonMessage(id, text, duration, col);
        }
    }
}