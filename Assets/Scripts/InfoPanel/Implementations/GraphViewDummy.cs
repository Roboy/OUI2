using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphViewDummy : View
{
    public override void UpdateView(Model model)
    {
        Debug.Log("View updated");
    }
}
