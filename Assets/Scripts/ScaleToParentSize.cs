using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToParentSize : MonoBehaviour
{
    //Should default to true
    public bool HasFixedScale;
    RectTransform parent;
    Vector2 size;

    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.GetComponent<RectTransform>();
        Rescale();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!HasFixedScale)
        {
            if(parent.sizeDelta != size)
            {
                Rescale();
            }
        }
    }

    private void Rescale()
    {
        size = parent.sizeDelta;
        this.transform.localScale = new Vector3(size.x, size.y, this.transform.localScale.z);
    }
}
