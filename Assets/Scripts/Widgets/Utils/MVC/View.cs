using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public abstract class View : MonoBehaviour
    {
        public abstract void Init(Model model);

        public abstract void UpdateView(Model model);
    }
}
