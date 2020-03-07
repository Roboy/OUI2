using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTorsoRotation : MonoBehaviour
{
    Transform torso;

    // Start is called before the first frame update
    void Start()
    {
        torso = GameObject.FindGameObjectWithTag("KatVRWalker").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, torso.rotation, Time.deltaTime * 100);
    }
}
