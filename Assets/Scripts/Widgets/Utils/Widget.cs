using UnityEngine;

namespace Widgets
{
    public class Widget : MonoBehaviour
    {
        private int id;

        private Controller controller;
        private Model model;
        private View view;

        private Context context;

        public void InitializeWidget(Controller controller, Model model, View view, Context context)
        {
            this.controller = controller;
            this.model = model;
            this.view = view;
            this.context = context;
            id = context.template_ID;
        }

        public int GetID()
        {
            return id;
        }

        public void ForwardRosMessage(RosJsonMessage rosMessage)
        {
            controller.ReceiveMessage(rosMessage);
        }
    }
}
