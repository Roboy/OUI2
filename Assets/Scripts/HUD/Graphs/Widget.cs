using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget : MonoBehaviour
{
    private int id;

    private Controller controller;
    private Model model;
    private View view;


    public void InitializeWidget(Controller controller, Model model, View view)
    {
        this.controller = controller;
        this.model = model;
        this.view = view;
        //TODO: probably not wanted
        view.Init(model);
    }
    
    public int GetID()
    {
        return id;
    }
}
