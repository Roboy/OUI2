using UnityEngine;

namespace Widgets
{
    public class Widget : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private int panel_id;

        private Controller controller;
        private Model model;
        private View view;

        private RosJsonMessage context;

        public void InitializeWidget(Controller controller, Model model, View view, RosJsonMessage RosJsonMessage)
        {
            this.controller = controller;
            this.model = model;
            this.view = view;
            this.context = RosJsonMessage;
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

        public void ForwardRosMessage(RosJsonMessage rosMessage)
        {
            controller.ReceiveMessage(rosMessage);
            view.UpdateView(model);
        }
    }
}
