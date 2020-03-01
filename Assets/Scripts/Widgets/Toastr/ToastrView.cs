using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO!!
namespace Widgets
{
    public class ToastrView : View
    {
        // private TextFieldManager textFieldManager;

        public override void Init(Model model, int panel_id)
        {
            this.model = model;

        }

        public void Update()
        {

        }

        public void NewToastr()
        {
            Debug.Log("new toastr");
        }

        public void DeleteToastr(Toastr toastrToDelete)
        {
            Debug.Log("deleted toastr");

            Destroy(toastrToDelete.gameObject);

        }

        public override void UpdateView(Model model)
        {
            
        }
    }
}