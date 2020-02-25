namespace Widgets
{
    public abstract class Controller
    {
        protected Model model;

        public Controller(Model model)
        {
            this.model = model;
        }

        public abstract void ReceiveMessage(RosJsonMessage rosMessage);
    }
}