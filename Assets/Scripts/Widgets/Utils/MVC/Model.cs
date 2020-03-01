using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public abstract class Model : MonoBehaviour
    {
        protected View view;

        public string title;

        public void Init(View view, string title)
        {
            this.view = view;
            this.title = title;
        }
    }
}
