﻿using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class ToastrWidget : Widget
    {
        public Queue<ToastrTemplate> toastrToInstantiateQueue;
        public Queue<ToastrTemplate> toastrActiveQueue;

        public float duration;
        public Color color;
        public int fontSize;

        public float timer;

        ToastrView view;

        public override void ProcessRosMessage(RosJsonMessage rosMessage)
        {
            EnqueueNewMessage(rosMessage.toastrMessage, rosMessage.toastrDuration, WidgetUtility.BytesToColor(rosMessage.toastrColor), rosMessage.toastrFontSize);
        }

        private void Awake()
        {
            toastrToInstantiateQueue = new Queue<ToastrTemplate>();
            toastrActiveQueue = new Queue<ToastrTemplate>();
            ResetTimer();
        }

        public new void Init(RosJsonMessage rosJsonMessage)
        {
            duration = rosJsonMessage.toastrDuration;
            color = WidgetUtility.BytesToColor(rosJsonMessage.toastrColor);
            fontSize = rosJsonMessage.toastrFontSize;

            base.Init(rosJsonMessage);
        }

        private void EnqueueNewMessage(string toastrMessage, float toastrDuration, Color toastrColor, int toastrFontSize)
        {
            if (toastrMessage.Equals(""))
            {
                return;
            }

            if (toastrColor == null)
            {
                toastrColor = color;
            }

            if (toastrDuration == 0.0f)
            {
                toastrDuration = duration;
            }

            if (toastrFontSize == 0)
            {
                toastrFontSize = fontSize;
            }

            toastrToInstantiateQueue.Enqueue(new ToastrTemplate(toastrMessage, toastrDuration, toastrColor, toastrFontSize));
        }   

        public void Update()
        {
            if (IsHudActive())
            {
                if (IsToastrTemplateInQueue() && view != null)
                {
                    ToastrTemplate toastrToInstantiate = toastrToInstantiateQueue.Dequeue();
                    toastrActiveQueue.Enqueue(toastrToInstantiate);
                    view.CreateNewToastr(toastrToInstantiate);
                }

                if (toastrActiveQueue.Count != 0)
                {
                    timer += Time.deltaTime;

                    if (timer >= toastrActiveQueue.Peek().toastrDuration)
                    {
                        view.DestroyTopToastr();
                        toastrActiveQueue.Dequeue();                        
                        ResetTimer();
                    }
                }
            }

            // This is true, everytime the HUD scene changes
            if (view == null)
            {
                GameObject toastrParent = GameObject.FindGameObjectWithTag("Panel_" + GetPanelID());
                if (toastrParent != null)
                {
                    RestoreViews(toastrParent);
                }
            }
        }

        private void ResetTimer()
        {
            timer = 0;
        }

        private bool IsHudActive()
        {
            return (AdditiveSceneManager.GetCurrentScene() == Scenes.HUD);
        }

        private bool IsToastrTemplateInQueue()
        {
            return (toastrToInstantiateQueue.Count != 0);
        }

        public override void RestoreViews(GameObject viewParent)
        {
            ResetTimer();
            view = viewParent.AddComponent<ToastrView>();
            view.Init(toastrActiveQueue.ToArray());
        }

        public class ToastrTemplate
        {
            public string toastrMessage;
            public Color toastrColor;
            public int toastrFontSize;
            public float toastrDuration;

            public ToastrTemplate(string toastrMessage, float toastrDuration, Color toastrColor, int toastrFontSize)
            {
                this.toastrMessage = toastrMessage;
                this.toastrColor = toastrColor;
                this.toastrFontSize = toastrFontSize;
                this.toastrDuration = toastrDuration;
            }
        }
    }
}
