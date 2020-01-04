using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget : MonoBehaviour
{
    private int id;

    private Controller controller;
    private Model model;
    private View view;

    private WidgetContext context;

    public void InitializeWidget(Controller controller, Model model, View view, WidgetContext context)
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

    public void ForwardRosMessage(byte[] rosMessageData)
    {
        controller.ReceiveMessage(rosMessageData);
    }
}
