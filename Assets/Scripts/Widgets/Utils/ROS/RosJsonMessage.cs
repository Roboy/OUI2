using System;

namespace Widgets
{
    [Serializable]
    public class RosJsonMessage
    {
        public int id;
        public float datapoint;
        public long timestamp;
        public int color;

        public RosJsonMessage(int id, float datapoint, long timestamp, int color)
        {
            this.id = id;
            this.datapoint = datapoint;
            this.timestamp = timestamp;
            this.color = color;
        }
    }
}