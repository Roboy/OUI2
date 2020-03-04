using UnityEngine;

public class Timer 
{
    private float duration;
    public float timer;

    public delegate void TimeIsUp();
    private TimeIsUp timeIsUp;    

    public void LetTimePass(float deltaTime)
    {
        timer += deltaTime;

        if (timer > duration)
        {
            timeIsUp();
        }
    }

    public float GetFraction()
    {
        return (timer / duration);
    }

    public void SetTimer(float duration, TimeIsUp timeIsUp)
    {
        this.duration = duration;
        timer = 0;
        this.timeIsUp = timeIsUp;
    }

    public void ResetTimer()
    {
        timer = 0;
    }
}
