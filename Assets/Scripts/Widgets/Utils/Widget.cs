﻿using UnityEngine;

namespace Widgets
{
    public class Widget : MonoBehaviour
    {
        private int id;

        private Controller controller;
        private Model model;
        private View view;

        private RosJsonMessage RosJsonMessage;

        public void InitializeWidget(Controller controller, Model model, View view, RosJsonMessage RosJsonMessage)
        {
            this.controller = controller;
            this.model = model;
            this.view = view;
            this.RosJsonMessage = RosJsonMessage;
            id = RosJsonMessage.id;
        }

        public int GetID()
        {
            return id;
        }

        public void ForwardRosMessage(RosJsonMessage rosMessage)
        {
            controller.ReceiveMessage(rosMessage);
            view.UpdateView(model);
        }
    }
}
