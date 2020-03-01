using UnityEngine;

namespace Widgets
{
    public abstract class Controller : MonoBehaviour
    {
        protected Model model;

        public void Init(Model model)
        {
            this.model = model;
        }

        public abstract void ReceiveMessage(RosJsonMessage rosMessage);
    }
}