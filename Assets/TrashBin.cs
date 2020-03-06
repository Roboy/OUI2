using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Trash") && !other.GetComponent<SenseGlove_Grabable>().IsInteracting())
        {
            Destroy(other.gameObject);
        }
    }
}
