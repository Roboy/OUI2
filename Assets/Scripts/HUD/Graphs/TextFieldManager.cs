﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{
    public class TextFieldManager : MonoBehaviour
    {
        public Queue<Toastr> queue = new Queue<Toastr>();

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
            }
            else
            {
                if (queue.Count > 0)
                {
                    Toastr p = queue.Dequeue();
                    text.text = p.msg;
                    timer = p.duration;
                    text.color = p.color;
                    text.fontSize = p.fontSize;
                }
                else
                {
                    // TODO: let the text vanish, e.g. shrink
                    text.text = "";
                }
            }
        }
    }

    public class Toastr
    {
        public string msg;
        public float duration;
        public Color color;
        public int fontSize;

        public Toastr(string msg, float duration, Color color, int fontSize)
        {          
            this.msg = msg;
            this.duration = duration;
            this.color = color;
            this.fontSize = fontSize;
        }
    }
}