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
        public int panel_id;
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

        #region TaskbarIcon
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

        public static RosJsonMessage CreateGraphMessage(int id, float graphDatapoint, int timestamp, byte[] graphColor)
        {
            return new RosJsonMessage(id, graphDatapoint, timestamp, graphColor);
        }
    }
}