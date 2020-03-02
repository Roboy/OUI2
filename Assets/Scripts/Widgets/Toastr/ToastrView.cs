using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Widgets.ToastrWidget;

namespace Widgets
{
    public class ToastrView : MonoBehaviour
    {
        public readonly int OFFSET = 40;
        public Queue<Toastr> toastrQueue;

        private void Awake()
        {
            toastrQueue = new Queue<Toastr>();
        }

        public void Init(ToastrTemplate[] activeToastr)
        {
            foreach (ToastrTemplate toastrTemplate in activeToastr)
            {
                CreateNewToastr(toastrTemplate);
            }
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

            foreach (Toastr toastr in toastrQueue)
            {
                toastr.transform.localPosition += OFFSET * Vector3.up;
            }
        }

        public void DestroyTopToastr()
        {
            Destroy(toastrQueue.Dequeue().gameObject);
            MoveToastrsUp();
        }

    }
}