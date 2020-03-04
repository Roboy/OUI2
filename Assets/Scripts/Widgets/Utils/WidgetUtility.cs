using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public static class WidgetUtility
    {
        public static Color BytesToColor(byte[] b)
        {
            if (b == null || b.Length == 0)
            {
                return new Color32(255, 255, 255, 255);
            }
            return new Color32(b[0], b[1], b[2], b[3]);
        }

        public static Color IntToColor(uint i)
        {
            byte[] b = BitConverter.GetBytes(i);
            return new Color32(b[0], b[1], b[2], b[3]);
        }

        public static Color IntToColor(int i)
        {
            byte[] b = BitConverter.GetBytes(i);
            return new Color32(b[0], b[1], b[2], b[3]);
        }

        /// <summary>
        /// Takes a string of format "FFFFFFFF" and returns the corresponding int value, which can be turned into a color using IntToColor()
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static uint HexStringToInt(string hexString)
        {
            return uint.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        }

        public static WidgetPosition StringToWidgetPosition(string widgetPositionAsString)
        {
            switch (widgetPositionAsString)
            {
                case "top":
                    return WidgetPosition.Top;

                case "left":
                    return WidgetPosition.Left;

                case "right":
                    return WidgetPosition.Right;

                case "bottom":
                    return WidgetPosition.Bottom;

                case "center":
                    return WidgetPosition.Center;

                case "child":
                    return WidgetPosition.Child;

                default:
                    Debug.LogWarning("Widget position " + widgetPositionAsString + " not known.");
                    return WidgetPosition.Incorrect;
            }
        }
    }

    public enum WidgetPosition { Top, Left, Right, Center, Bottom, Child, Incorrect };
}