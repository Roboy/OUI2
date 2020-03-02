using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Widgets
{
    public class GUIData : Singleton<GUIData>
    {
        public GameObject[] positionsUI;
        public GameObject[] positionsDetailedPanels;
        public GameObject[] positionsText;
        public GameObject[] positionsAdvancedMenu;
        public Sprite[] icons;
    }
}