using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Widgets.ToastrWidget;

namespace Widgets
{
    public class ToastrView : View
    {
        public readonly int OFFSET = 40;
        public Queue<Toastr> toastrQueue;

        private void Awake()
        {
            toastrQueue = new Queue<Toastr>();
            
        }

        public override void Init(Widget widget)
        {
            SetChildWidget(widget.childWidget);

            foreach (ToastrTemplate toastrTemplate in ((ToastrWidget)widget).toastrActiveQueue.ToArray())
            {
                CreateNewToastr(toastrTemplate);
            }

            AttachCurvedUI();
        }

        public void CreateNewToastr(ToastrTemplate toastrToInstantiate)
        {
            GameObject toastrGameObject = new GameObject();
            toastrGameObject.transform.SetParent(transform, false);
            toastrGameObject.transform.localPosition -= toastrQueue.Count * OFFSET * Vector3.up;

            toastrGameObject.name = toastrToInstantiate.toastrMessage;
            Toastr newToastr = toastrGameObject.gameObject.AddComponent<Toastr>();
            newToastr.Init(toastrToInstantiate.toastrMessage, toastrToInstantiate.toastrColor, toastrToInstantiate.toastrFontSize);

            toastrQueue.Enqueue(newToastr);
        }

        public void MoveToastrsUp()
        {
            // TODO: A nice slerp like toastr.SlerpUp()

            float offsetTime = 0;

            foreach (Toastr toastr in toastrQueue)
            {
                // toastr.transform.localPosition += OFFSET * Vector3.up;
                offsetTime += 0.1f;
                toastr.SlerpUp(OFFSET, offsetTime);
            }
        }

        public void DestroyTopToastr()
        {
            Destroy(toastrQueue.Dequeue().gameObject);
            MoveToastrsUp();
        }

        // TODO: hide new toastr as well
        public override void ShowView()
        {

        }

        // TODO: hide new toastr as well
        public override void HideView()
        {
            Debug.LogWarning("Toastr widget can not be hidden.");
        }

    }
}