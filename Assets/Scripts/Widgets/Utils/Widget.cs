using UnityEngine;

namespace Widgets
{
    public abstract class Widget : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private int panel_id;

        private RosJsonMessage context;

        public void Init(RosJsonMessage RosJsonMessage)
        {
            context = RosJsonMessage;
            id = RosJsonMessage.id;
            panel_id = RosJsonMessage.panel_id;
        }

        public int GetID()
        {
            return id;
        }

        public int GetPanelID()
        {
            return panel_id;
        }

        public RosJsonMessage GetContext()
        {
            return context;
        }

        public abstract void ProcessRosMessage(RosJsonMessage rosMessage);
    }
}
