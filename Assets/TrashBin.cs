using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Trash") && !other.GetComponent<SenseGlove_Grabable>().IsInteracting())
        {
            //Destroy(other.gameObject);
            other.transform.SetParent(this.transform);
            other.transform.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.4f, 0.07f), Random.Range(-0.14f, 0.1f));
            other.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        }
    }
}
