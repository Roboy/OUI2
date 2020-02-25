﻿using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public abstract class Model
    {
        protected View view;

        protected int panel_id;

        public string title;

        public Model(View view, string title, int panel_id)
        {
            this.view = view;
            this.panel_id = panel_id;
            this.title = title;
        }

        public int GetPanelId()
        {
            return panel_id;
        }

        protected static int ProcessInitialValue(int value, int defaultValue, bool allowNegValues, string variableName)
        {
            if (value == 0)
            {
                return defaultValue;
            }
            else if (!allowNegValues && value < 0)
            {
                Debug.LogWarning("Invalid value for " + variableName + ": " + value);
                return defaultValue;
            }
            else
            {
                return value;
            }
        }

        protected static float ProcessInitialValue(float value, float defaultValue, bool allowNegValues, string variableName)
        {
            if (Mathf.Abs(value) <= 0.01f)
            {
                return defaultValue;
            }
            else if (!allowNegValues && value < 0)
            {
                Debug.LogWarning("Invalid value for " + variableName + ": " + value);
                return defaultValue;
            }
            else
            {
                return value;
            }
        }
    }
}
