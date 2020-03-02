using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TransitionManager : MonoBehaviour
{
    PostProcessVolume postProcessVolume;
    
    [SerializeField]
    private float overallDuration = 1.5f;
    [SerializeField]
    private bool isLerping = false;

    private int curDirection = 1;

    private float lerpingValue;
    private float startValue = 0.0f, endValue = 1.0f;
    private float curTime = 0;

    void Start()
    {
        this.postProcessVolume = GetComponent<PostProcessVolume>();
    }

    /// <summary>
    /// Listens on [isLerping] to perform visual transition.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            StartTransition();
        }
        if (isLerping)
        {
            curTime += Time.deltaTime;

            float flow = curTime / overallDuration;

            if (flow < 1)
            {
                lerpingValue = startValue + (endValue - startValue) * flow;
                this.postProcessVolume.weight = lerpingValue;
            }
            else
            {
                lerpingValue = startValue;
                float tmp = this.startValue;
                this.startValue = this.endValue;
                this.endValue = tmp;
                this.curTime = 0;

                if (curDirection == 1)
                {
                    curDirection = -1;
                } else if (curDirection == -1)
                {
                    curDirection = 1;
                    isLerping = false;
                }
            }
        }
    }

    /// <summary>
    /// Starts visual transition animation between scenes.
    /// </summary>
    public void StartTransition()
    {
        this.isLerping = true;
        GetComponent<VestPlayer>().playTact();
    }
}
