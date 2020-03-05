using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Widgets.ToastrWidget;

namespace Widgets
{
    public class ToastrView : View
    {
        public readonly int OFFSET = 110;
        public Queue<Toastr> toastrQueue;

        public GameObject toastrDesignPrefab;

        private void Awake()
        {
            toastrQueue = new Queue<Toastr>();
        }

        public override void Init(Widget widget)
        {
            SetChildWidget(widget.childWidget);

            toastrDesignPrefab = widget.viewDesignPrefab;

            foreach (ToastrTemplate toastrTemplate in ((ToastrWidget)widget).toastrActiveQueue.ToArray())
            {
                CreateNewToastr(toastrTemplate);
            }

            Init(widget.relativeChildPosition);
        }

        public void CreateNewToastr(ToastrTemplate toastrToInstantiate)
        {
            GameObject toastrGameObject = Instantiate(toastrDesignPrefab);
            toastrGameObject.transform.SetParent(transform, false);
            toastrGameObject.transform.localPosition -= toastrQueue.Count * OFFSET * Vector3.up;

            toastrGameObject.name = toastrToInstantiate.toastrMessage;

            Toastr newToastr = toastrGameObject.GetComponent<Toastr>();
            newToastr.Init(toastrToInstantiate.toastrMessage, toastrToInstantiate.toastrColor, toastrToInstantiate.toastrFontSize);

            toastrQueue.Enqueue(newToastr);
        }

        public void MoveToastrsUp()
        {
            float offsetTime = 0;

            foreach (Toastr toastr in toastrQueue)
            {
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
        public override void ShowView(RelativeChildPosition relativeChildPosition)
        {

        }

        // TODO: hide new toastr as well
        public override void HideView()
        {
            Debug.LogWarning("Toastr widget can not be hidden.");
        }

    }
}