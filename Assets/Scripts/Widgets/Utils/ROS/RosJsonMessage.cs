using System;

namespace Widgets
{
    [Serializable]
    public class RosJsonMessage
    {
        public int id;

        #region General        
        public string title;
        public string type;
        public int panelId;
        public int childWidgetId;
        public int timestamp;
        #endregion

        #region Graph
        public float graphDatapoint;
        public byte[] graphColor;
        #endregion

        #region Toastr
        public string toastrMessage;
        public int toastrFontSize;
        public byte[] toastrColor;
        public float toastrDuration;
        #endregion

        #region Text
        public string textMessage;
        public int textFontSize;
        #endregion

        #region Icon
        public string currentIcon;
        public string[] icons;
        #endregion
        

        private RosJsonMessage(int id, float graphDatapoint, int timestamp, byte[] graphColor)
        {
            this.id = id;
            this.graphDatapoint = graphDatapoint;
            this.graphColor = graphColor;
            this.timestamp = timestamp;
        }

        private RosJsonMessage(int id, string currentIcon)
        {
            this.id = id;
            this.currentIcon = currentIcon;
        }

        public RosJsonMessage(int id, string title, string type, int panelId, string toastrMessage, int toastrFontSize, byte[] toastrColor, float toastrDuration) : this(id, title)
        {
            this.type = type;
            this.panelId = panelId;
            this.toastrMessage = toastrMessage;
            this.toastrFontSize = toastrFontSize;
            this.toastrColor = toastrColor;
            this.toastrDuration = toastrDuration;
        }

        public static RosJsonMessage CreateGraphMessage(int id, float graphDatapoint, int timestamp, byte[] graphColor)
        {
            return new RosJsonMessage(id, graphDatapoint, timestamp, graphColor);
        }

        public static RosJsonMessage CreateIconMessage(int id, string currentIcon)
        {
            return new RosJsonMessage(id, currentIcon);
        }

        public static RosJsonMessage CreateToastrMessage(int id, string text)
        {
            return new RosJsonMessage(id, text, null, 0, text, 0, null, 0);
        }
    }
}