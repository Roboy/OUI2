using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public abstract class View : MonoBehaviour
    {
        public abstract void Init(Model model, int panel_id);

        public abstract void UpdateView(Model model);
    }
}
