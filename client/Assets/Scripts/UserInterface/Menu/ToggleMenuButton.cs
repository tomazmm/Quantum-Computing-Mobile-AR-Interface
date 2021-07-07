using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class ToggleMenuButton : MonoBehaviour
    {
        public void setMenuState()
        {
            if (Canvas.toggleMenu)
                Canvas.toggleMenu = false;
            else
                Canvas.toggleMenu = true;
        }
    }
}

