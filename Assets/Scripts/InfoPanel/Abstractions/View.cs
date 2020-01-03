using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract void Init();

    public abstract void UpdateView(Model model);
}
