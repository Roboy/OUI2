using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFieldManager : MonoBehaviour
{
    Queue<Printable> queue = new Queue<Printable>();

    private float timer;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            if (queue.Count > 0)
            {
                Printable p = queue.Dequeue();
                text.text = p.msg;
                timer = p.dur;
                text.color = p.col;
                text.fontSize = p.fontSize;
            }
        }
    }
}

public struct Printable 
{
    public string msg;
    public float dur;
    public Color col;
    public byte fontSize;
}